using UnityEngine;

public class SoundRender : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    RenderTexture B;
  //  RenderTexture D;
    //RenderTexture C;
    public Material material;
    public Texture Rought;
    int handle_main;
    int handle_main2;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;
    public float RoughtIntensity;
    public float Low;
    public float TLow;
    public float SLow;
    public float Mid;
    public float TMid;
    public float SMid;
    public float High;
    public float THigh;
    public float SHigh;
    public int _resx;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;
    public render script;
    public float f1;
    public float f2;
    float p1;
    float p2;
    //public Material Nebula;
    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();
        B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.enableRandomWrite = true;
        B.Create();

        handle_main = compute_shader.FindKernel("CSMain");
        handle_main2 = compute_shader.FindKernel("CSMain2");
    }

    void Update()
    {

        p1 = Mathf.Lerp(p1, f1, 0.02f);
        p2 = Mathf.Lerp(p2, f2, 0.02f);
        compute_shader.SetFloat("_RoughtIntensity", RoughtIntensity);
        compute_shader.SetTexture(handle_main2, "reader2", Rought);
        compute_shader.SetTexture(handle_main2, "reader", A);
        compute_shader.SetFloat("_p1", p1);
        compute_shader.SetFloat("_p2",p2);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        compute_shader.SetFloat("_Low", Low);
        compute_shader.SetFloat("_SLow", SLow);
        compute_shader.SetFloat("_TLow", TLow);
        compute_shader.SetFloat("_Mid", Mid);
        compute_shader.SetFloat("_SMid", SMid);
        compute_shader.SetFloat("_TMid", TMid);
        compute_shader.SetFloat("_High", High);
        compute_shader.SetFloat("_SHigh", SHigh);
        compute_shader.SetFloat("_THigh", THigh);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);

        compute_shader.SetTexture(handle_main2, "writer", B);
        compute_shader.Dispatch(handle_main2, B.width / 8, B.height / 8, 1);
        

        material.SetTexture("_SunShaft",B);
        script.C = B;

    }
}