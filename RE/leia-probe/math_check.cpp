#define main unused_observer_entrypoint
#include "probe.cpp"
#undef main
#include <numbers>

// This entrypoint invokes mathematics only; no SR context or SDK method is called.
int main() {
    const double halfPi=std::numbers::pi/2;
    double worst=0;size_t checks=0;
    auto check=[&](SR_point3d actual,SR_point3d expected){
        worst=std::max(worst,distance(actual,expected));++checks;
    };
    check(rotateVector({1,0,0},{0,0,halfPi}),{0,1,0});
    check(rotateVector({0,1,0},{halfPi,0,0}),{0,0,1});
    check(rotateVector({0,0,1},{0,halfPi,0}),{1,0,0});
    check(rotateVector({3,-4,12},{0,0,0}),{3,-4,12});
    for(int i=-5;i<=5;++i)for(int j=-3;j<=3;++j){
        SR_point3d v{i*0.41,j*0.29,(i-j)*0.13},p{double(i*7),double(j*9),double(i*j-11)};
        const double a=std::hypot(v.x,v.y,v.z);
        if(a<1e-12)continue;
        const auto u=v/a; const double c=std::cos(a),s=std::sin(a),d=1-c;
        // Explicit Rodrigues matrix, independently evaluated from vector/cross implementation.
        const double m[3][3]{
            {c+u.x*u.x*d,u.x*u.y*d-u.z*s,u.x*u.z*d+u.y*s},
            {u.y*u.x*d+u.z*s,c+u.y*u.y*d,u.y*u.z*d-u.x*s},
            {u.z*u.x*d-u.y*s,u.z*u.y*d+u.x*s,c+u.z*u.z*d}};
        SR_point3d expected{};
        for(int r=0;r<3;++r)for(int k=0;k<3;++k)expected.p[r]+=m[r][k]*p.p[k];
        auto actual=rotateVector(p,v);
        check(actual,expected);
        check(rotateVector(actual,v*(-1)),p);
        check({distance(actual,{0,0,0}),0,0},{distance(p,{0,0,0}),0,0});
    }
    std::cout<<"math_only_checks="<<checks<<" maximum_error="<<std::setprecision(16)<<worst<<"\n";
    return worst<1e-10?0:1;
}
