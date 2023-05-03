Shader "Custom RP/Unlit"
{
    Properties
    {
        //[可选：特性]变量名(Inspector上的文本,类型名) = 默认值
        //[optional: attribute] name("display text in Inspector", type name) = default value

         //"white"为默认纯白贴图，{}在很久之前用于纹理的设置
        _BaseMap("Texture", 2D) = "white"{}


        _BaseColor("Color",Color) = (1.0,1.0,1.0,1.0)

        //混合模式使用的值，其值应该是枚举值，但是这里使用float
        //特性用于在Editor下更方便编辑
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend",Float) = 1
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend",Float) = 0
        //深度写入模式
        [Enum(Off,0,On,1)] _ZWrite("Z Write",Float) = 1//

        //Clip的Shader关键字，启用该Toggle会将_Clipping关键字添加到该材质活动关键字列表中，而禁用该Toggle会将其删除
        [Toggle(_CLIPPING)] _Clipping("Alpha Clipping",Float) = 0
    }

    SubShader
    {
        Pass
        {
             //设置混合模式
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

            HLSLPROGRAM
            //不生成OpenGL ES 2.0等图形API的着色器变体，其不支持可变次数的循环与线性颜色空间
            #pragma target 3.5
            //这一指令会让Unity生成两个该Shader的变体，一个支持GPU Instancing，另一个不支持。
            #pragma multi_compile_instancing

            #pragma vertex UnlitPassVertex
            #pragma fragment UnlitPassFragment
            //告诉Unity启用_CLIPPING关键字时编译不同版本的Shader
            #pragma shader_feature _CLIPPING


            #include "UnlitPass.hlsl"
            ENDHLSL
        }
    }
    CustomEditor "CustomShaderGUI"
}