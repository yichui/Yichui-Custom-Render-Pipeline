//用来定义光源属性
#ifndef CUSTOM_LIGHT_INCLUDED
#define CUSTOM_LIGHT_INCLUDED


//使用宏定义最大方向光源数，需要与cpu端匹配
#define MAX_DIRECTIONAL_LIGHT_COUNT 4

//用CBuffer包裹构造方向光源的两个属性，cpu会每帧传递（修改）这两个属性到GPU的常量缓冲区，对于一次渲染过程这两个值恒定
CBUFFER_START(_CustomLight)
//当前有效光源数
int _DirectionalLightCount;
//注意CBUFFER中创建数组的格式,在Shader中数组在创建时必须明确其长度，创建完毕后不允许修改
float4 _DirectionalLightColors[MAX_DIRECTIONAL_LIGHT_COUNT];
float4 _DirectionalLightDirections[MAX_DIRECTIONAL_LIGHT_COUNT];
CBUFFER_END

struct Light
{
    //光源颜色
    float3 color;
    //光源方向：指向光源
    float3 direction;
};

int GetDirectionalLightCount()
{
    return _DirectionalLightCount;
}

//返回一个配置好的光源，初始化为Color白色，光线从上垂直向下投射（不明确坐标系，但由于教程中在世界空间下计算光照，因此这里多半指的是世界空间）
// Light GetDirectionalLight()
// {
//     Light light;
//     light.color = 1.0;
//     light.direction = float3(0.0,1.0,0.0);
//     return light;
// }

//返回一个方向光源，其颜色与方向取自常量缓冲区，cpu每帧会对该缓冲区赋值或修改
Light GetDirectionalLight(int index)
{
    Light light;
    //float4的rgb和xyz完全等效
    light.color = _DirectionalLightColors[index].rgb;
    light.direction = _DirectionalLightDirections[index].xyz;
    return light;
}


#endif