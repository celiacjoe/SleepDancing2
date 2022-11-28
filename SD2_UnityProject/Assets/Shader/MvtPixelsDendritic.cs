using UnityEngine;

public class MvtPixelsDendritic : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    RenderTexture B;
    RenderTexture D;
    RenderTexture E;
    public Material material;
    public WebCamTexture C;
   // public RenderTexture C;
    int handle_main;
    int handle_main2;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;
    [Range(0, 1)]
    public float m1;
    [Range(0, 1)]
    public float m2;
    [Range(-1, 1)]
    public float dir;
    public int _resx;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;
    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();
        B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.enableRandomWrite = true;
        B.Create();
        C = new WebCamTexture();
        C.Play();  
        E = new RenderTexture(_resx, _resy, 0, rtFormat);
        E.enableRandomWrite = true;
        E.Create();
        D = new RenderTexture(_resx, _resy, 0, rtFormat);
        D.enableRandomWrite = true;
        D.Create();
        handle_main = compute_shader.FindKernel("CSMain");
        handle_main2 = compute_shader.FindKernel("CSMain2");
    }

    void Update()
    {
       
        compute_shader.SetTexture(handle_main, "reader", A);
        compute_shader.SetTexture(handle_main, "reader2", C);
        compute_shader.SetTexture(handle_main2, "reader", D);
        compute_shader.SetTexture(handle_main2, "reader2", C);
        compute_shader.SetTexture(handle_main2, "reader3", B);
        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_taille", Taille);
        compute_shader.SetFloat("_forme", Forme);
        compute_shader.SetFloat("_disparition", Disparition);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        compute_shader.SetFloat("_m1", m1);
        compute_shader.SetFloat("_m2", m2);
        compute_shader.SetFloat("_dir", dir);
        compute_shader.SetTexture(handle_main, "writer", B);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        compute_shader.SetTexture(handle_main, "reader", B);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        compute_shader.SetTexture(handle_main2, "writer", E);
        compute_shader.Dispatch(handle_main2, E.width / 8, E.height / 8, 1);
        compute_shader.SetTexture(handle_main2, "reader", E);
        compute_shader.SetTexture(handle_main2, "writer", D);
        compute_shader.Dispatch(handle_main2, E.width / 8, E.height / 8, 1);
        material.SetTexture("_MainTex", E);
    }
}