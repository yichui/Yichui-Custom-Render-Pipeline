using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
	//定义分部函数的方式类似C++
	partial void DrawUnsupportedShaders();
	//这块代码只会在Editor下起作用
	#if UNITY_EDITOR
	//获取Unity默认的shader tag id
	private static ShaderTagId[] legacyShaderTagIds =
	{
		new ShaderTagId("Always"),
		new ShaderTagId("ForwardBase"),
		new ShaderTagId("PrepassBase"),
		new ShaderTagId("Vertex"),
		new ShaderTagId("VertexLMRGBM"),
		new ShaderTagId("VertexLM")
	};

	//Error Material
	private static Material errorMaterial;
	partial void DrawUnsupportedShaders()
	{
		//获取Error材质
		if (errorMaterial == null)
		{
			errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));
		}
		//绘制走不支持的Shader Pass的物体
		var drawingSettings = new DrawingSettings(legacyShaderTagIds[0], new    SortingSettings(camera))
		{
			//设置覆写的材质
			overrideMaterial = errorMaterial
		};

		//设置更多在此次DrawCall中要渲染的ShaderPass，也就是不支持的ShaderPass
		for (int i = 1; i < legacyShaderTagIds.Length; i++)
		{
			drawingSettings.SetShaderPassName(i, legacyShaderTagIds[i]);
		}
		var filteringSettings = FilteringSettings.defaultValue;
		context.DrawRenderers(cullingResults, ref drawingSettings, ref  filteringSettings);
	}
	#endif
}