using UnityEngine;
using UnityEngine.Rendering;
public class CameraRenderer : MonoBehaviour
{
    //存放当前渲染上下文
    private ScriptableRenderContext context;

    //存放摄像机渲染器当前应该渲染的摄像机
    private Camera camera;

    //摄像机渲染器的渲染函数，在当前渲染上下文的基础上渲染当前摄像机
    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;


        DrawVisibleGeometry();
        Submit();
    }

    void DrawVisibleGeometry()
    {
        //添加“绘制天空盒”指令，DrawSkybox为ScriptableRenderContext下已有函数，这里就体现了为什么说Unity已经帮我们封装好了很多我们要用到的函数，SPR的画笔~
        context.DrawSkybox(camera);
    }

    void Submit()
    {
        //提交当前上下文中缓存的指令队列，执行指令队列
        context.Submit();
    }
}
