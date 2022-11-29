using UnityEngine;

public class VideoDeform : MonoBehaviour
{
    public ComputeShader compute_shader;
    public ComputeShader compute_shader2;
    public ComputeShader compute_shader3;
    public Material material;
    public Material material2;
    RenderTexture A;
    RenderTexture B;
    RenderTexture F;
    RenderTexture G;
    public WebCamTexture C;
    public Texture D;
    public Texture E;
    int handle_main;
    int handle_main2;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;
    [Range(0, 1)]
    public float RoughtIntensity;
    [Range(0, 1)]
    public float m1;
    [Range(0, 1)]
    public float m2;
    [Range(-1, 1)]
    public float dir;
    public int _resx;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;

    public render script;
    public bool volume;
    public bool Dendritic;
    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();
        B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.wrapMode = TextureWrapMode.Mirror;
        B.enableRandomWrite = true;
        B.Create();
        F = new RenderTexture(_resx, _resy, 0, rtFormat);
        F.enableRandomWrite = true;
        F.Create();
        G = new RenderTexture(_resx, _resy, 0, rtFormat);
        G.enableRandomWrite = true;
        G.Create();
        handle_main =  compute_shader.FindKernel("CSMain");
        handle_main2 = compute_shader.FindKernel("CSMain2");
        WebCamDevice[] devices = WebCamTexture.devices;
        C = new WebCamTexture(devices[0].name);
        C.Play();       
    }

    void Update()
    {

        if (volume == false && Dendritic == false)
        {
            compute_shader.SetTexture(handle_main, "reader2", C);
            compute_shader.SetTexture(handle_main2, "reader", A);
            compute_shader.SetTexture(handle_main2, "reader2", D);
            compute_shader.SetFloat("_time", Time.time);
            compute_shader.SetFloat("_taille", Taille);
            compute_shader.SetFloat("_forme", Forme);
            compute_shader.SetFloat("_disparition", Disparition);
            compute_shader.SetFloat("_m1", m1);
            compute_shader.SetFloat("_m2", m2);
            compute_shader.SetFloat("_resx", _resx);
            compute_shader.SetFloat("_resy", _resy);
            compute_shader.SetFloat("_RoughtIntensity", RoughtIntensity);
            compute_shader.SetTexture(handle_main, "writer", A);
            compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);
            compute_shader.SetTexture(handle_main2, "writer2", B);
            compute_shader.Dispatch(handle_main2, B.width / 8, B.height / 8, 1);
            material.SetTexture("_Cam", B);
            script.C = B;
            script.video = true;
        }
         else
        {
            if (Dendritic == false && volume == true)
            {
                compute_shader2.SetTexture(handle_main, "reader", A);
                compute_shader2.SetTexture(handle_main, "reader2", C);
                compute_shader2.SetTexture(handle_main, "reader3", E);
                compute_shader2.SetFloat("_time", Time.time);
                compute_shader2.SetFloat("_taille", Taille);
                compute_shader2.SetFloat("_forme", Forme);
                compute_shader2.SetFloat("_disparition", Disparition);
                compute_shader2.SetFloat("_resx", _resx);
                compute_shader2.SetFloat("_resy", _resy);
                compute_shader2.SetTexture(handle_main, "writer", B);
                compute_shader2.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
                compute_shader2.SetTexture(handle_main, "reader", B);
                compute_shader2.SetTexture(handle_main, "writer", A);
                compute_shader2.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
                material2.SetTexture("_Name", B);
            }
            else
            {
                compute_shader3.SetTexture(handle_main, "reader", A);
                compute_shader3.SetTexture(handle_main, "reader2", C);
                compute_shader3.SetTexture(handle_main2, "reader", F);
                compute_shader3.SetTexture(handle_main2, "reader2", C);
                compute_shader3.SetTexture(handle_main2, "reader3", G);
                compute_shader3.SetFloat("_time", Time.time);
                compute_shader3.SetFloat("_taille", Taille);
                compute_shader3.SetFloat("_forme", Forme);
                compute_shader3.SetFloat("_disparition", Disparition);
                compute_shader3.SetFloat("_resx", _resx);
                compute_shader3.SetFloat("_resy", _resy);
                compute_shader3.SetFloat("_m1", m1);
                compute_shader3.SetFloat("_m2", m2);
                compute_shader3.SetFloat("_dir", dir);
                compute_shader3.SetTexture(handle_main, "writer", G);
                compute_shader3.Dispatch(handle_main, G.width / 8, G.height / 8, 1);
                compute_shader3.SetTexture(handle_main, "reader",G);
                compute_shader3.SetTexture(handle_main, "writer", A);
                compute_shader3.Dispatch(handle_main, G.width / 8, G.height / 8, 1);
                compute_shader3.SetTexture(handle_main2, "writer", B);
                compute_shader3.Dispatch(handle_main2, B.width / 8, B.height / 8, 1);
                compute_shader3.SetTexture(handle_main2, "reader", B);
                compute_shader3.SetTexture(handle_main2, "writer", F);
                compute_shader3.Dispatch(handle_main2, B.width / 8, B.height / 8, 1);
                material.SetTexture("_Cam", B);
            }
        }
    }
}