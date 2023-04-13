using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        /*在RenderPipeline的实例中，Unity每一帧都会执行其Render函数，
         * 该函数传入了一个ScriptableRenderContext类型的context用于连接引擎底层，
         * 我们用它来实际进行渲染，粗暴来说，每帧内所有渲染相关的信息都存放在context中，
         * 同时该函数传入一个摄像机数组，很好理解，意思是我们要在当前帧按顺序渲染这些摄像机拍到的画面。*/

        foreach (Camera camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }
}
