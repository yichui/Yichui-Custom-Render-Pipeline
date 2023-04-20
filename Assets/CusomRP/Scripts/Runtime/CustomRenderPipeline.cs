using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    //摄像机渲染器实例，用于管理所有摄像机的渲染
    private CameraRenderer renderer = new CameraRenderer();

    #region 批处理配置
    private bool useDynamicBatching;
    private bool useGPUInstancing;
    #endregion

    //构造函数，初始化管线的一些属性
    public CustomRenderPipeline(bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher)
    {

        this.useDynamicBatching = useDynamicBatching;
        this.useGPUInstancing = useGPUInstancing;
        //配置SRP Batch
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
    }

    //必须重写Render函数，渲染管线实例每帧执行Render函数
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        /*在RenderPipeline的实例中，Unity每一帧都会执行其Render函数，
         * 该函数传入了一个ScriptableRenderContext类型的context用于连接引擎底层，
         * 我们用它来实际进行渲染，粗暴来说，每帧内所有渲染相关的信息都存放在context中，
         * 同时该函数传入一个摄像机数组，很好理解，意思是我们要在当前帧按顺序渲染这些摄像机拍到的画面。*/
        //按顺序渲染每个摄像机
        foreach (Camera camera in cameras)
        {
            renderer.Render(context, camera, useDynamicBatching, useGPUInstancing);
        }
    }
}
