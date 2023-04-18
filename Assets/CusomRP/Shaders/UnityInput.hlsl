//存储Shader中的一些常用的输入数据
#ifndef CUSTOM_UNITY_INPUT_INCLUDED
#define CUSTOM_UNITY_INPUT_INCLUDED

float4x4 unity_ObjectToWorld;//内置着色器变量,HLSLPROGRAM中需要我们自己声明一些获取内置着色器变量

float4x4 unity_MatrixVP;//内置着色器变量,HLSLPROGRAM中需要我们自己声明一些获取内置着色器变量

#endif