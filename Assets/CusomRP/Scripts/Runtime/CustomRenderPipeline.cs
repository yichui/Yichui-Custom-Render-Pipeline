using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    //������дRender������Ŀǰ�����ڲ�ʲô����ִ��
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        /*��RenderPipeline��ʵ���У�Unityÿһ֡����ִ����Render������
         * �ú���������һ��ScriptableRenderContext���͵�context������������ײ㣬
         * ����������ʵ�ʽ�����Ⱦ���ֱ���˵��ÿ֡��������Ⱦ��ص���Ϣ�������context�У�
         * ͬʱ�ú�������һ����������飬�ܺ���⣬��˼������Ҫ�ڵ�ǰ֡��˳����Ⱦ��Щ������ĵ��Ļ��档*/
    }
}
