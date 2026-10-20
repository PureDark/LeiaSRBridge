#pragma once
#include <algorithm>
#include <array>
#include <cmath>

namespace titan::pose {
using Vec = std::array<double,3>;
using Mat = std::array<double,9>; // row major, columns are local head axes
inline Vec Add(Vec a,Vec b){return {a[0]+b[0],a[1]+b[1],a[2]+b[2]};}
inline Vec Sub(Vec a,Vec b){return {a[0]-b[0],a[1]-b[1],a[2]-b[2]};}
inline Vec Scale(Vec a,double s){return {a[0]*s,a[1]*s,a[2]*s};}
inline double Dot(Vec a,Vec b){return a[0]*b[0]+a[1]*b[1]+a[2]*b[2];}
inline double Length(Vec a){return std::sqrt(Dot(a,a));}
inline Vec Cross(Vec a,Vec b){return {a[1]*b[2]-a[2]*b[1],a[2]*b[0]-a[0]*b[2],a[0]*b[1]-a[1]*b[0]};}
inline bool Unit(Vec& a){const auto n=Length(a);if(!std::isfinite(n)||n<1e-9)return false;a=Scale(a,1/n);return true;}
inline Vec Transform(const Mat& m,Vec p){
    return {m[0]*p[0]+m[1]*p[1]+m[2]*p[2],m[3]*p[0]+m[4]*p[1]+m[5]*p[2],m[6]*p[0]+m[7]*p[1]+m[8]*p[2]};
}
inline Mat Rodrigues(Vec r){
    const double angle=Length(r);
    if(angle<1e-12)return {1,0,0,0,1,0,0,0,1};
    r=Scale(r,1/angle);const double c=std::cos(angle),s=std::sin(angle),t=1-c;
    const auto [x,y,z]=r;
    return {t*x*x+c,t*x*y-s*z,t*x*z+s*y,t*x*y+s*z,t*y*y+c,t*y*z-s*x,t*x*z-s*y,t*y*z+s*x,t*z*z+c};
}
inline bool IsRotation(const Mat& m){
    for(auto n:m)if(!std::isfinite(n))return false;
    const Vec x{m[0],m[3],m[6]},y{m[1],m[4],m[7]},z{m[2],m[5],m[8]};
    return std::abs(Dot(x,x)-1)<1e-5&&std::abs(Dot(y,y)-1)<1e-5&&
        std::abs(Dot(z,z)-1)<1e-5&&std::abs(Dot(x,y))<1e-5&&
        std::abs(Dot(x,z))<1e-5&&std::abs(Dot(y,z))<1e-5&&Dot(Cross(x,y),z)>.99999;
}
// Convert through a quaternion: stable near zero and pi, unlike theta/sin(theta).
inline Vec RotationVector(const Mat& m){
    double q[4]{};const double trace=m[0]+m[4]+m[8];
    if(trace>0){
        const double s=2*std::sqrt(trace+1);
        q[0]=s/4;q[1]=(m[7]-m[5])/s;q[2]=(m[2]-m[6])/s;q[3]=(m[3]-m[1])/s;
    }else{
        unsigned i=m[4]>m[0]?1u:0u;if(m[8]>m[i*3+i])i=2;
        const unsigned j=(i+1)%3,k=(i+2)%3;
        const double s=2*std::sqrt(std::max(0.0,1+m[i*3+i]-m[j*3+j]-m[k*3+k]));
        q[i+1]=s/4;q[0]=(m[k*3+j]-m[j*3+k])/s;
        q[j+1]=(m[j*3+i]+m[i*3+j])/s;q[k+1]=(m[k*3+i]+m[i*3+k])/s;
    }
    if(q[0]<0)for(auto& v:q)v=-v;
    Vec axis{q[1],q[2],q[3]};const double n=Length(axis);
    return n<1e-12?Scale(axis,2):Scale(axis,2*std::atan2(n,q[0])/n);
}
}
