using UnityEngine;
using UnityEngine.Rendering;
public class CameraRenderer : MonoBehaviour
{
    //��ŵ�ǰ��Ⱦ������
    private ScriptableRenderContext context;

    //����������Ⱦ����ǰӦ����Ⱦ�������
    private Camera camera;

    //�������Ⱦ������Ⱦ�������ڵ�ǰ��Ⱦ�����ĵĻ�������Ⱦ��ǰ�����
    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;


        DrawVisibleGeometry();
        Submit();
    }

    void DrawVisibleGeometry()
    {
        //��ӡ�������պС�ָ�DrawSkyboxΪScriptableRenderContext�����к����������������Ϊʲô˵Unity�Ѿ������Ƿ�װ���˺ܶ�����Ҫ�õ��ĺ�����SPR�Ļ���~
        context.DrawSkybox(camera);
    }

    void Submit()
    {
        //�ύ��ǰ�������л����ָ����У�ִ��ָ�����
        context.Submit();
    }
}
