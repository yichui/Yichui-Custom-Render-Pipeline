//定义BRDF函数需要传入的参数
#ifndef CUSTOM_BRDF_INCLUDED
#define CUSTOM_BRDF_INCLUDED

struct BRDF
{
    //物体表面漫反射颜色
    float3 diffuse;
    //物体表面高光颜色
    float3 specular;
    //物体表面粗糙度
    float3 roughness;
};

BRDF GetBRDF(Surface surface)
{
    BRDF brdf;
    //diffuse等于物体表面反射的光能量color
    brdf.diffuse = surface.color;
    //暂时使用固定值
    brdf.specular = 0.0;
    brdf.roughness = 1.0;
    return brdf;
}

#endif