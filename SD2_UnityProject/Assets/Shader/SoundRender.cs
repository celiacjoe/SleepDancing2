using UnityEngine;

public class SoundRender : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
  //  RenderTexture B;
  //  RenderTexture D;
    //RenderTexture C;
    public Material material;
   // public Texture C;
    int handle_main;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;
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
    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();
      /*  B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.enableRandomWrite = true;
        B.Create();
        /* C = new RenderTexture(1920, 1080, 0);
         C.enableRandomWrite = true;
         C.Create();    */
        /*D = new RenderTexture(_resx, _resy, 0);
        D.enableRandomWrite = true;
        D.Create();   */
        handle_main = compute_shader.FindKernel("CSMain");

    }

    void Update()
    {

       
        
       //     compute_shader.SetTexture(handle_main, "reader", A);
      //  compute_shader.SetTexture(handle_main, "reader2", C);
        /*compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_taille", Taille);
        compute_shader.SetFloat("_forme", Forme);
        compute_shader.SetFloat("_disparition", Disparition);    */
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
        //compute_shader.SetFloat("_delta", Time.deltaTime);
        // compute_shader.SetTexture(handle_main, "writer", B);
        //compute_shader.SetTexture(handle_main, "writer2", C);
        compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);
        //compute_shader.SetTexture(handle_main, "reader", B);
        compute_shader.SetTexture(handle_main, "writer", A);
       // compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);
       // compute_shader.SetTexture(handle_main, "writer2", D);
        material.SetTexture("_MainTex",A);
    }
}