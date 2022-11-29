using UnityEngine;

public class ShapeRender : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    public Material materialVisu;
    public render script;
    public Material material;

    int handle_main;
    public float masque;
    public float focal;
    public float distance;
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float smoothForm;
    public float complexity;
    public float taille;
    public float position1;
    public float position2;
    public Vector3 modifforme01;
    public Vector3 modifforme02;
    public float detail;
    public int _resx;
    public float p1;
    public float p2;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;
    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();      
  
        handle_main = compute_shader.FindKernel("CSMain");
        
    }

    void Update()
    {
        p1 = Mathf.Lerp(p1, position1, 0.02f);
        p2 = Mathf.Lerp(p2, position2, 0.02f);
        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_masque", masque);
        compute_shader.SetFloat("_focal", focal);
        compute_shader.SetFloat("_p1", p1);
        compute_shader.SetFloat("_p2", p2);
        compute_shader.SetFloat("_distance", distance);
        compute_shader.SetFloat("_rotateX", rotateX);
        compute_shader.SetFloat("_rotateY", rotateY);
        compute_shader.SetFloat("_rotateZ", rotateZ);
        compute_shader.SetFloat("_smoothForm", smoothForm);
        compute_shader.SetFloat("_complexity", complexity);
        compute_shader.SetFloat("_taille", taille);
        compute_shader.SetVector("_modifforme01", modifforme01);
        compute_shader.SetVector("_modifforme02", modifforme02);
        compute_shader.SetFloat("_detail", detail);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);
        materialVisu.SetTexture("_Preview", A);
        material.SetTexture("_3DShapeDeform", A);
        script.C = A;

    }
}