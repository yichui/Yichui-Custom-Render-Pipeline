using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

//用于把场景中的光源信息通过cpu传递给gpu
public class Lighting
{
    private const string bufferName = "Lighting";

   
    //主要使用到CullingResults下的光源信息
    CullingResults cullingResults;

    //最大方向光源数量
    private const int maxDirLightCount = 4;

    //获取CBUFFER中对应数据名称的Id，CBUFFER就可以看作Shader的全局变量吧
    // private static int dirLightColorId = Shader.PropertyToID("_DirectionalLightColor"),
    //     dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");
    //改为传递Vector4数组+当前传递光源数
    private static int dirLightCountId = Shader.PropertyToID("_DirectionalLightCount");
    private static int dirLightColorsId = Shader.PropertyToID("_DirectionalLightColors");
    private static int dirLightDirectionsId = Shader.PropertyToID("_DirectionalLightDirections");

    private static Vector4[] dirLightColors = new Vector4[maxDirLightCount];
    private static Vector4[] dirLightDirections = new Vector4[maxDirLightCount];

    Shadows shadows = new Shadows();

    private CommandBuffer buffer = new CommandBuffer()
    {
        name = bufferName
    };

    //传入参数context用于注入CmmandBuffer指令，cullingResults用于获取当前有效的光源信息
    public void Setup(ScriptableRenderContext context, CullingResults cullingResults, ShadowSettings shadowSettings)
    {
        this.cullingResults = cullingResults;

        //对于传递光源数据到GPU的这一过程，我们可能用不到CommandBuffer下的指令（其实用到了buffer.SetGlobalVector），但我们依然使用它来用于Debug
        buffer.BeginSample(bufferName);
        //SetupDirectionalLight();

        shadows.Setup(context, cullingResults, shadowSettings);

        SetupLights();

        buffer.EndSample(bufferName);
        //再次提醒这里只是提交CommandBuffer到Context的指令队列中，只有等到context.Submit()才会真正依次执行指令
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

     //配置Vector4数组中的单个属性
    //传进的visibleLight添加了ref关键字，防止copy整个VisibleLight结构体（该结构体空间很大）
    void SetupDirectionalLight(int index,ref VisibleLight visibleLight)
    {
        //VisibleLight.finalColor为光源颜色（实际是光源颜色*光源强度，但是默认不是线性颜色空间，需要将Graphics.lightsUseLinearIntensity设置为true）
        dirLightColors[index] = visibleLight.finalColor;
        //光源方向为光源localToWorldMatrix的第三列，这里也需要取反
        dirLightDirections[index] = -visibleLight.localToWorldMatrix.GetColumn(2);
    }
 
    void SetupLights()
    {
        //使用NativeArray类型局部变量来接收cullingResults.visibleLights。
        //NaviteArray是一个类似于数组的结构，但是提供了与Native Memory buffer（本机内存缓冲区）的连接，
        //从而无任何编组成本，它使得托管C#数组与Native Unity引擎代码更高效地共享数据
        //在官方文档中提到，在后台，NativeArrays提供的系统允许将它们安全地用于作业，并自动跟踪内存泄漏。
        //NativeArray只是个容器，不包含任何数据或内存，但它包含指向本机堆上的C++端创建的本机C++对象的句柄或指针
        //所以实际的数组不在托管内存中，不受GC影响，NativeArray本质上包含对本机对象的内部阴影，如果没有正确处理它们，这些对象就会丢失引用。
        //因此，NativeArray实际指向了原生的C++数组。此外，在Unity JobSystem、ECS体系中也会使用到NativeArray。
        //简单来说，NativeArray的好处就是：分配C++数组，不需要GC，生成销毁成本低等。
        NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
        //循环配置两个Vector数组
        int dirLightCount = 0;
        for (int i = 0; i < visibleLights.Length; i++)
        {
            VisibleLight visibleLight = visibleLights[i];

            //只配置方向光源
            if (visibleLight.lightType == LightType.Directional)
            {
                //设置数组中单个光源的属性
                SetupDirectionalLight(dirLightCount++, ref visibleLight);
                if (dirLightCount >= maxDirLightCount)
                {
                    //最大不超过4个方向光源
                    break;
                }
            }
        }

        //传递当前有效光源数、光源颜色Vector数组、光源方向Vector数组。
        buffer.SetGlobalInt(dirLightCountId, visibleLights.Length);
        buffer.SetGlobalVectorArray(dirLightColorsId, dirLightColors);
        buffer.SetGlobalVectorArray(dirLightDirectionsId, dirLightDirections);
    }

}