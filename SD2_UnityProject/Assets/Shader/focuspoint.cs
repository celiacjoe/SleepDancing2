using UnityEngine;

public class focuspoint : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    RenderTexture B;
    public RenderTexture C;
    public Material material;
    int handle_main;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;

    public int _resx;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;
    void Start()
    {
       
        A = new RenderTexture(_resx, _resy, 0, rtFormat);
        B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.enableRandomWrite = true;
        B.Create();
        A.enableRandomWrite = true;
        A.Create();
        handle_main = compute_shader.FindKernel("CSMain");
        
    }

    void Update()
    {
        compute_shader.SetTexture(handle_main, "reader", A);
        compute_shader.SetTexture(handle_main, "reader2", C);
        compute_shader.SetTexture(handle_main, "writer", B);
        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_taille", Taille);
        compute_shader.SetFloat("_forme", Forme);
        compute_shader.SetFloat("_disparition", Disparition);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        compute_shader.SetTexture(handle_main, "reader", B);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        //B.filterMode = FilterMode.Point;
        material.SetTexture("_MainTex", B);
    }
}