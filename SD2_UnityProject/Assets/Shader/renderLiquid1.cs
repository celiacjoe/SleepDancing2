using UnityEngine;

public class renderLiquid1 : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    RenderTexture B;
    //RenderTexture D;
    //RenderTexture C;
    public Material material;
    public RenderTexture C;
    //public WebCamTexture C;
    int handle_main;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Forme1;
    [Range(0, 1)]
    public float Forme2;
    [Range(0, 1)]
    public float Forme3;
    [Range(0, 1)]
    public float Forme4;
    [Range(0, 1)]
    public float Forme5;
    [Range(0, 1)]
    public float Forme6;
    [Range(0, 1)]
    public float Disparition;

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
        /* C = new RenderTexture(1920, 1080, 0);
         C.enableRandomWrite = true;
         C.Create();    */
       /* D = new RenderTexture(_resx, _resy, 0);
        D.enableRandomWrite = true;
        D.Create();  */
        handle_main = compute_shader.FindKernel("CSMain");
       /* C = new WebCamTexture();
        handle_main = compute_shader.FindKernel("CSMain");
        C.Play();  */

    }

    void Update()
    {
       
        compute_shader.SetTexture(handle_main, "reader", A);
        compute_shader.SetTexture(handle_main, "reader2", C);
        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_taille", Taille);
        compute_shader.SetFloat("_forme", Forme);
        compute_shader.SetFloat("_forme1", Forme1);
        compute_shader.SetFloat("_forme2", Forme2);
        compute_shader.SetFloat("_forme3", Forme3);
        compute_shader.SetFloat("_forme4", Forme4);
        compute_shader.SetFloat("_forme5", Forme5);
        compute_shader.SetFloat("_forme6", Forme6);
        compute_shader.SetFloat("_disparition", Disparition);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        //compute_shader.SetFloat("_delta", Time.deltaTime);
        compute_shader.SetTexture(handle_main, "writer", B);
        //compute_shader.SetTexture(handle_main, "writer2", C);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        compute_shader.SetTexture(handle_main, "reader", B);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        //compute_shader.SetTexture(handle_main, "writer2", D);
        material.SetTexture("_MainTex", B);
    }
}