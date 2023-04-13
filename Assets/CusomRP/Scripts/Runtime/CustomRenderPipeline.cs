using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    //必须重写Render函数，目前函数内部什么都不执行
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {

    }
}
