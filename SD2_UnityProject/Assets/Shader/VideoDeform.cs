using UnityEngine;

public class VideoDeform : MonoBehaviour
{
    public ComputeShader compute_shader;
    public Material material;

    RenderTexture A;
    RenderTexture B;
    
    public WebCamTexture C;

    RenderTexture D;
    RenderTexture E;

    int handle_main;
    int handle_main2;
    int handle_main3;

    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;

    public int _resx;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resy;

    public render script;


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
        D.Create();
        E = new RenderTexture(_resx, _resy, 0);
        E.enableRandomWrite = true;
        E.Create();   */
        handle_main =  compute_shader.FindKernel("CSMain");
        handle_main2 = compute_shader.FindKernel("CSMain2");
       // handle_main3 = compute_shader.FindKernel("CSMain3");
        C = new WebCamTexture();
        C.Play();  

    }

    void Update()
    {
       
       // compute_shader.SetTexture(handle_main, "reader", A);
        compute_shader.SetTexture(handle_main, "reader2", C);
        compute_shader.SetTexture(handle_main2, "reader", A);
       // compute_shader.SetTexture(handle_main3, "reader3", B);
       // compute_shader.SetTexture(handle_main3, "reader4", D);
        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_taille", Taille);
        compute_shader.SetFloat("_forme", Forme);
        compute_shader.SetFloat("_disparition", Disparition);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);        
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);
        compute_shader.SetTexture(handle_main2, "writer2", B);
        compute_shader.Dispatch(handle_main2, B.width / 8, B.height / 8, 1);
       /* compute_shader.SetTexture(handle_main3, "writer3", E);
        compute_shader.Dispatch(handle_main3, E.width / 8, E.height / 8, 1);
        compute_shader.SetTexture(handle_main3, "reader4", E);
        compute_shader.SetTexture(handle_main3, "writer3", D);
        compute_shader.Dispatch(handle_main3, E.width / 8, E.height / 8, 1);     */
        material.SetTexture("_Video", B);
        script.C = B;
        // material.SetTexture("_DeformTex", E);
        // material.SetTexture("_MainTex2", A);

    }
}