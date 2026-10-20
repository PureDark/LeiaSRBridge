#include "broker/HeadPose.h"
#include "common/BoeFacePoints.h"
#include <opencv2/calib3d.hpp>
#include <iostream>
#include <stdexcept>

using namespace titan;
namespace {
void Require(bool ok,const char* message){if(!ok)throw std::runtime_error(message);}
const std::array<double,9> left{1100,1080,640,360,-.12,.03,.001,-.002,.002};
const std::array<double,9> right{1120,1110,638,362,-.10,.02,-.001,.001,.001};
const std::array<double,10> stereo{1000,640,360,60,.01,-.02,.003,-.012,.015,-.004};
cv::Mat Rotation(pose::Vec vector){
    cv::Mat matrix;cv::Rodrigues(cv::Vec3d(vector[0],vector[1],vector[2]),matrix);return matrix;
}
double Error(const pose::Mat& actual,const cv::Mat& expected){
    cv::Mat a(3,3,CV_64F,const_cast<double*>(actual.data())),vector;
    cv::Rodrigues(a*expected.t(),vector);return cv::norm(vector)*180/3.141592653589793;
}
Snapshot State(){
    Snapshot s{};s.calibrationValid=s.eyePositionValid=1;s.refractParams[0]=s.refractParams[1]=s.refractParams[2]=1;
    return s;
}
BoeTrackingFrame Project(pose::Vec r,pose::Vec t={15,-10,650}){
    const cv::Mat rotation=Rotation(r);
    const std::array<pose::Vec,5> model{{{-32,0,0},{32,0,0},{0,-25,-20},{-25,-50,0},{25,-50,0}}};
    BoeTrackingFrame frame{};frame.sdkResult=1;frame.centerValid=frame.pairValid=1;
    frame.facePointsValid=1;
    frame.sequence=1;frame.observationTick=100;frame.processId=123;frame.processCreationTime=456;
    std::int32_t pixels[2][5][2]{};
    for(unsigned i=0;i<5;++i){
        cv::Mat p=rotation*cv::Mat(cv::Vec3d(model[i][0],model[i][1],model[i][2]));
        for(int axis=0;axis<3;++axis)p.at<double>(axis)+=t[axis];
        for(unsigned cam=0;cam<2;++cam){
            const auto& c=cam?right:left;
            cv::Mat rect=Rotation({stereo[4+3*cam],stereo[5+3*cam],stereo[6+3*cam]});
            // Independent OpenCV forward camera model: inverse rectification,
            // lens distortion and truncation to the vendor's public int ABI.
            cv::Mat ray=rect.t()*cv::Mat(cv::Vec3d(
                (-p.at<double>(0)+(cam?-30.0:30.0))/p.at<double>(2),
                -p.at<double>(1)/p.at<double>(2),1));
            std::vector<cv::Point3d> object{{ray.at<double>(0),ray.at<double>(1),ray.at<double>(2)}};
            std::vector<cv::Point2d> image;
            cv::Mat k=(cv::Mat_<double>(3,3)<<c[0],0,c[2],0,c[1],c[3],0,0,1);
            cv::Mat d(1,5,CV_64F,const_cast<double*>(c.data()+4));
            cv::projectPoints(object,cv::Vec3d(0,0,0),cv::Vec3d(0,0,0),k,d,image);
            pixels[cam][i][0]=static_cast<int>(image[0].x);
            pixels[cam][i][1]=static_cast<int>(image[0].y);
            frame.facePoints[cam][i][0]=static_cast<float>(image[0].x);
            frame.facePoints[cam][i][1]=static_cast<float>(image[0].y);
        }
    }
    std::memcpy(frame.raw+84,pixels[0],sizeof(pixels[0]));
    std::memcpy(frame.raw+124,pixels[1],sizeof(pixels[1]));
    return frame;
}
}
int main(){
    try{
        broker::HeadPose solver;Require(solver.Configure(left,right,stereo),"Valid calibration rejected");
        auto s=State();double maxError=0;unsigned samples=0;
        for(int axis=0;axis<3;++axis)for(int angle=-40;angle<=40;angle+=10){
            pose::Vec r{};r[axis]=angle*3.141592653589793/180;
            for(double z:{450.0,650.0,900.0}){
                auto f=Project(r,{15,-10,z});pose::Mat rotation{};
                Require(solver.Solve(f,s,rotation),"Projected face rejected");
                const double error=Error(rotation,Rotation(r));maxError=std::max(maxError,error);
                Require(error<.01,"Stereo head pose does not recover known rotation");
                const auto vector=pose::RotationVector(rotation);
                Require(Error(pose::Rodrigues(vector),Rotation(vector))<1e-5,"Rodrigues disagrees with OpenCV");
                ++samples;
            }
        }
        auto f=Project({.15,-.22,.1});pose::Mat baseline{},rotated{};
        Require(solver.Solve(f,s,baseline),"Combined rotation failed");
        s.wholeMat[0]=7;s.wholeMat[1]=-5;s.wholeMat[2]=9;
        s.wholeMat[3]=100;s.wholeMat[4]=-50;s.wholeMat[5]=30;
        const double rad=3.141592653589793/180;
        cv::Mat whole=Rotation({7*rad,0,0})*Rotation({0,-5*rad,0})*Rotation({0,0,9*rad});
        Require(solver.Solve(f,s,rotated),"Display transform rejected");
        Require(Error(rotated,whole*cv::Mat(3,3,CV_64F,baseline.data()))<.001,"Pose wholeMat order/sign wrong");
        s.eye[0]=1000; // compensation/prediction must not feed head orientation
        pose::Mat compensated{};Require(solver.Solve(f,s,compensated),"Compensated pose failed");
        Require(Error(compensated,cv::Mat(3,3,CV_64F,rotated.data()))<.001,"Position compensation changed pose");
        for(const auto r:{pose::Vec{0,0,0},pose::Vec{3.141592653589793,0,0},pose::Vec{0,-3.141592653589793,0},
                pose::Vec{0,0,3.141592653589793},pose::Vec{1e-10,0,0},pose::Vec{1.7,1.4,-2.1}}){
            const auto m=pose::Rodrigues(r),round=pose::Rodrigues(pose::RotationVector(m));
            Require(pose::IsRotation(m)&&Error(round,Rotation(r))<1e-5,"Rotation-vector zero/pi round trip failed");
        }
        pose::Mat bad{-1,0,0,0,1,0,0,0,1};Require(!pose::IsRotation(bad),"Reflection accepted as rotation");
        s=State();solver.Update(s,&f,100);Require(s.headPoseValid,"No pose published");
        auto corrupt=f;std::memset(corrupt.facePoints,0,sizeof(corrupt.facePoints));
        solver.Update(s,&corrupt,110);Require(s.headPoseValid&&s.headPoseObservationTick==100,"Duplicate SDK frame was recomputed or retimed");
        corrupt.sequence=2;corrupt.observationTick=120;solver.Update(s,&corrupt,120);
        Require(!s.headPoseValid,"Bad new face retained old orientation");
        f.sequence=3;f.observationTick=130;solver.Update(s,&f,130);Require(s.headPoseValid,"Reacquisition failed");
        solver.Update(s,&f,381);Require(!s.headPoseValid,"Stale frame has valid rotation");
        solver.Update(s,&f,129);Require(!s.headPoseValid,"Future frame accepted");
        for(auto valid:{kEyeHeld,kEyeDefault,0u}){
            s.eyePositionValid=valid;solver.Update(s,&f,130);Require(!s.headPoseValid,"Fallback eye fabricated rotation");
        }
        s=State();solver.Update(s,&f,130);Require(s.headPoseValid,"Reset recovery failed");
        auto newProcess=corrupt;newProcess.sequence=f.sequence;newProcess.processCreationTime++;
        solver.Update(s,&newProcess,130);Require(!s.headPoseValid,"Reused sequence from another process used cached pose");
        auto mismatched=Project({0,0,0});mismatched.facePoints[1][0][1]+=15;
        Require(!solver.Solve(mismatched,s,bad),"Epipolar mismatch accepted");
        auto noSubpixel=Project({0,0,0});noSubpixel.facePointsValid=0;
        Require(!solver.Solve(noSubpixel,s,bad),"Integer landmarks silently used as accurate pose");
        // Exercise the exact private vector reader with bounded synthetic work
        // storage, including bad pointers and mismatch with the public result.
        auto copied=Project({.1,.2,.1});std::array<std::uint8_t,216> work{};
        for(unsigned cam=0;cam<2;++cam){
            const auto first=reinterpret_cast<std::uintptr_t>(copied.facePoints[cam]);
            const std::uintptr_t vector[]{first,first+40,first+40};
            std::memcpy(work.data()+168+cam*24,vector,sizeof(vector));
        }
        Require(CopyBoeFacePoints(work.data(),copied)&&copied.facePointsValid,"Same-frame float copy failed");
        copied.raw[84]^=4;
        Require(!CopyBoeFacePoints(work.data(),copied)&&!copied.facePointsValid,"Mismatched float/int frame accepted");
        Require(!CopyBoeFacePoints(reinterpret_cast<void*>(1),copied),"Bad private pointer was not contained");
        auto wrongCal=stereo;wrongCal[3]=0;
        Require(!solver.Configure(left,right,wrongCal)&&!solver.Solve(f,s,bad),"Zero baseline accepted");
        std::cout<<"HeadPose PASS: "<<samples<<" stereo projections; max error "<<maxError
            <<" deg with subpixel landmarks; OpenCV reference; wholeMat, private copy, lifecycle and invalidity.\n";
        return 0;
    }catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}
}
