using UnityEngine;

public class mouvementDetection : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    RenderTexture B;
    public WebCamTexture C;

    public Material material;
    int handle_main;
    [Range(0, 1)]
    public float Taille;
    [Range(0, 1)]
    public float Forme;
    [Range(0, 1)]
    public float Disparition;
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resx;    
    public int _resy;

    void Start()
    {
        C = new WebCamTexture();
        A = new RenderTexture(_resx, _resy, 0, rtFormat);
        A.enableRandomWrite = true;
        A.Create();
        B = new RenderTexture(_resx, _resy, 0, rtFormat);
        B.enableRandomWrite = true;
        B.Create();
 
        handle_main = compute_shader.FindKernel("CSMain");
        C.Play();
    }

    void Update()
    {
       // material.SetTexture("_CamTex", C);
        compute_shader.SetTexture(handle_main, "reader", A);
        compute_shader.SetTexture(handle_main, "reader2", C);
        
        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_taille", Taille);
        compute_shader.SetFloat("_forme", Forme);
        compute_shader.SetFloat("_disparition", Disparition);
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        compute_shader.SetTexture(handle_main, "writer", B);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        compute_shader.SetTexture(handle_main, "reader", B);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
        //material.SetTexture("_MainTex", B);
        gameObject.GetComponent<focuspoint>().C = B;




    }
}