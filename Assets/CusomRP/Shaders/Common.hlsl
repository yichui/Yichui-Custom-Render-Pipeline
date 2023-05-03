//存储一些常用的函数，如空间变换
#ifndef CUSTOM_COMMON_INCLUDED
#define CUSTOM_COMMON_INCLUDED

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "UnityInput.hlsl"

//将Unity内置着色器变量转换为SRP库需要的变量
#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject
#define UNITY_MATRIX_V unity_MatrixV
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection
//使用2021版本的坑，我们还需要定义两个PREV标识符，才不会报错，但这两个变量具体代表什么未知
#define UNITY_PREV_MATRIX_M unity_ObjectToWorld
#define UNITY_PREV_MATRIX_I_M unity_WorldToObject
//我们直接使用SRP库中已经帮我们写好的函数
//在include UnityInstancing.hlsl之前需要定义Unity_Matrix_M和其他宏，以及SpaceTransform.hlsl
//UnityInstancing.hlsl重新定义了一些宏用于访问实例化数据数组
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

float Square (float v) {
	return v * v;
}


#endif