using UnityEngine;

public class NebulaRender : MonoBehaviour
{
    public ComputeShader compute_shader;
    RenderTexture A;
    RenderTexture B;
    public Texture noise;
    public Material material;
    int handle_main;
    int handle_main2;
    [Space(20)]
    [Header("PositionSetting")]
    [Range(0, 1)]
    public float transition1;
    [Range(0, 1)]
    public float transition2;
    [Range(0, 1)]
    public float transition3;
    [Range(0, 1)]
    public float transition4;
    public Vector3 Pos;
    public Vector3 PosL;
    public float Focal;
    public float t1;
    public float t2;
    public float t3;
    public float t4;
    [Space(20)]
    [Header("ColorSetting")]
    [Range(0, 50)]
    public float reg1;
    [Range(0, 1)]
    public float reg2;
    [Range(0, 1)]
    public float reg3;
    [Range(0, 1)]
    public float reg4;
    [Range(0, 1)]
    public float Co1;
    [Range(0, 1)]
    public float Co2;
    [Range(0, 1)]
    public float Co3;
    [Range(0, 20)]
    public float Blur;
    [Range(0, 1)]
    public float Chro1;
    public Color VolumeColor;
    [Space(20)]
    [Header("SoundSetting")]
    public float Low;
    public float TLow;
    public float SLow;
    public float Mid;
    public float TMid;
    public float SMid;
    public float High;
    public float THigh;
    public float SHigh;
    public float f3;
    public float f1;
    public float f2;
    [Space(20)]
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
    public int _resx;
    public int _resy;
    public int _resx2;
    public int _resy2;
    public render script;
    float tr1;
    float tr2;
    float tr3;
    float tr4;
    float p1;
    float p2;
    float p3;

    void Start()
    {
        A = new RenderTexture(_resx, _resy, 0,rtFormat);
        A.enableRandomWrite = true;
        A.Create();
        B = new RenderTexture(_resx2, _resy2, 0, rtFormat);
        B.enableRandomWrite = true;
        B.Create();
        handle_main = compute_shader.FindKernel("CSMain");
        handle_main2 = compute_shader.FindKernel("CSMain2");
        compute_shader.SetFloat("_resx", _resx);
        compute_shader.SetFloat("_resy", _resy);
        compute_shader.SetFloat("_resx2", _resx2);
        compute_shader.SetFloat("_resy2", _resy2);
        compute_shader.SetFloat("_reg1", reg1);
        compute_shader.SetFloat("_reg2", reg2);
        compute_shader.SetFloat("_reg3", reg3);
        compute_shader.SetFloat("_reg4", reg4);
        compute_shader.SetFloat("_co1", Co1);
        compute_shader.SetFloat("_co2", Co2);
        compute_shader.SetFloat("_co3", Co3);
        compute_shader.SetVector("_VolumeColor", VolumeColor);
        //  compute_shader.SetTexture(handle_main, "reader3", C);
    }

    void Update()
    {
        tr1 = Mathf.Lerp(tr1, transition1, 0.01f);
        tr2 = Mathf.Lerp(tr2, transition2, 0.01f);
        tr3 = Mathf.Lerp(tr3, transition3, 0.01f);
        tr4 = Mathf.Lerp(tr4, transition4, 0.01f);
        p1 = Mathf.Lerp(p1, f1, 0.02f);
        p2 = Mathf.Lerp(p2, f2, 0.02f);
        p3 = Mathf.Lerp(p3, f3, 0.02f);
        compute_shader.SetTexture(handle_main, "reader", noise);
        compute_shader.SetTexture(handle_main2, "reader", noise);
        compute_shader.SetTexture(handle_main2, "reader2", A);
        compute_shader.SetFloat("_t1", t1);
        compute_shader.SetFloat("_t2", t2);
        compute_shader.SetFloat("_t3", t3);
        compute_shader.SetFloat("_t4", t4);
        //compute_shader.SetFloat("_f1", f1);
        //compute_shader.SetFloat("_f2", f2);
        compute_shader.SetFloat("_f3", p1);
        compute_shader.SetFloat("_f4", p2);
        compute_shader.SetFloat("_f5", p3);
        compute_shader.SetFloat("_transition1", tr1);
        compute_shader.SetFloat("_transition2", tr2);
        compute_shader.SetFloat("_transition3", tr3);
        compute_shader.SetFloat("_transition4", tr4);
        compute_shader.SetVector("_position", Pos);
        compute_shader.SetVector("_positionL", PosL);
        compute_shader.SetFloat("_focal", Focal);
        compute_shader.SetFloat("_chro1", Chro1);
        compute_shader.SetFloat("_blur", Blur);

        compute_shader.SetFloat("_Low", Low);
        compute_shader.SetFloat("_SLow", SLow);
        compute_shader.SetFloat("_TLow", TLow);
        compute_shader.SetFloat("_Mid", Mid);
        compute_shader.SetFloat("_SMid", SMid);
        compute_shader.SetFloat("_TMid", TMid);
        compute_shader.SetFloat("_High", High);
        compute_shader.SetFloat("_SHigh", SHigh);
        compute_shader.SetFloat("_THigh", THigh);

        compute_shader.SetFloat("_time", Time.time);
        compute_shader.SetFloat("_mousex", Input.mousePosition.x / Display.main.systemWidth);
        compute_shader.SetFloat("_mousey", Input.mousePosition.y / Display.main.systemHeight);
        compute_shader.SetTexture(handle_main, "writer", A);
        compute_shader.Dispatch(handle_main, A.width / 8, A.height / 8, 1);
        compute_shader.SetTexture(handle_main2, "writer", B);
        compute_shader.Dispatch(handle_main2, B.width / 8, B.height / 8, 1);
        material.SetTexture("_Nebula",B);
        script.C = B;
    }
}