using UnityEngine;
using UnityEngine.Rendering;

//用于把场景中的光源信息通过cpu传递给gpu
public class Lighting
{
    private const string bufferName = "Lighting";

    //获取CBUFFER中对应数据名称的Id，CBUFFER就可以看作Shader的全局变量吧
    private static int dirLightColorId = Shader.PropertyToID("_DirectionalLightColor");
    private static int dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");

    private CommandBuffer buffer = new CommandBuffer()
    {
        name = bufferName
    };

    public void Setup(ScriptableRenderContext context)
    {
        //对于传递光源数据到GPU的这一过程，我们可能用不到CommandBuffer下的指令（其实用到了buffer.SetGlobalVector），但我们依然使用它来用于Debug
        buffer.BeginSample(bufferName);
        SetupDirectionalLight();
        buffer.EndSample(bufferName);
        //再次提醒这里只是提交CommandBuffer到Context的指令队列中，只有等到context.Submit()才会真正依次执行指令
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    void SetupDirectionalLight()
    {
        //通过RenderSettings.sun获取场景中默认的最主要的一个方向光，我们可以在Window/Rendering/Lighting Settings中显式配置它。
        Light light = RenderSettings.sun;
        //使用CommandBuffer.SetGlobalVector将光源信息传递给GPU
        //该方法传递的永远是Vector4，即使我们传递的是Vector3，在传递过程中也会隐式转换成Vector4,然后在Shader读取时自动屏蔽掉最后一个分量
        buffer.SetGlobalVector(dirLightColorId, light.color.linear * light.intensity);
        //注意光源方向要取反再传递过去
        buffer.SetGlobalVector(dirLightDirectionId, -light.transform.forward);
    }
}