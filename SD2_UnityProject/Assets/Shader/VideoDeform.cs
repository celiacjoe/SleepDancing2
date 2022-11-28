using UnityEngine;

public class VideoDeform : MonoBehaviour
{
    public ComputeShader compute_shader;
    public ComputeShader compute_shader2;
    public Material material;
    public Material material2;
    RenderTexture A;
    RenderTexture B;
    public WebCamTexture C2;
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
    public int _resx;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;

    public render script;
    public bool volume;

    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();
        B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.wrapMode = TextureWrapMode.Mirror;
        B.enableRandomWrite = true;
        B.Create();
        handle_main =  compute_shader.FindKernel("CSMain");
        handle_main2 = compute_shader.FindKernel("CSMain2");
        WebCamDevice[] devices = WebCamTexture.devices;
        C2 = new WebCamTexture(devices[0].name);
        C2.Play();
        
    }

    void Update()
    {

        if (volume == false)
        {
            compute_shader.SetTexture(handle_main, "reader2", C2);
            compute_shader.SetTexture(handle_main2, "reader", A);
            compute_shader.SetTexture(handle_main2, "reader2", D);
            compute_shader.SetFloat("_time", Time.time);
            compute_shader.SetFloat("_taille", Taille);
            compute_shader.SetFloat("_forme", Forme);
            compute_shader.SetFloat("_disparition", Disparition);
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
            compute_shader2.SetTexture(handle_main, "reader", A);
            compute_shader2.SetTexture(handle_main, "reader2", C2);
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
    }
}