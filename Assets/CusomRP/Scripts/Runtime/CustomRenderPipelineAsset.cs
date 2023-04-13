using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    //��д����ʵ��RenderPipeline�ĺ���
    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipeline();
    }
}
