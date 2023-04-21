using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class MeshBall : MonoBehaviour
{
    //��֮ǰһ����ʹ��int���͵�PropertyId������������
    private static int baseColorId = Shader.PropertyToID("_BaseColor");

    //GPU Instancingʹ�õ�Mesh
    [SerializeField] private Mesh mesh = default;
    //GPU Instancingʹ�õ�Material
    [SerializeField] private Material material = default;

    //���ǿ���new 1000��GameObject����������Ҳ����ֱ��ͨ��ÿʵ������ȥ����GPU Instancing������
    //����ÿʵ������
    private Matrix4x4[] matrices = new Matrix4x4[1023];
    private Vector4[] baseColors = new Vector4[1023];

    private MaterialPropertyBlock block;

    private void Awake()
    {

        //for (int i = 0; i < matrices.Length; i++)
        //{
        //    //�ڰ뾶10�׵���ռ������ʵ��С���λ��
        //    matrices[i] = Matrix4x4.TRS(Random.insideUnitSphere * 10f, Quaternion.identity, Vector3.one);
        //    baseColors[i] = new Vector4(Random.value, Random.value, Random.value, 1f);
        //}

        for (int i = 0; i < matrices.Length; i++)
        {
            //�ڰ뾶10�׵���ռ������ʵ��С���λ��
            matrices[i] = Matrix4x4.TRS(Random.insideUnitSphere * 10f,
                Quaternion.Euler(Random.value * 360f, Random.value * 360f, Random.value * 360f),
                Vector3.one * Random.Range(0.5f, 1.5f));
            baseColors[i] = new Vector4(Random.value, Random.value, Random.value, Random.Range(0.5f, 1f));
        }
    }

    private void Update()
    {
        //����û�д���GameObject����Ҫÿ֡����
        if (block == null)
        {
            block = new MaterialPropertyBlock();
            //����������������
            block.SetVectorArray(baseColorId, baseColors);
        }

        //һ֡���ƶ�����񣬲���û�д�������Ҫ����Ϸ����Ŀ�����һ�����ֻ�ܻ���1023��ʵ���������ʱ���֧��GPU Instancing
        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 1023, block);
    }
}