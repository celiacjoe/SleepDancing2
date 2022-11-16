using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.VFX;


public class SceneManager : MonoBehaviour
{
    //public Texture m_Default, m_01, m_02;
    public InputMidiControl S_Midi;
    public Renderer R;
    public render QuadRender;
    public ComputeShader CS_Default, CS02, CS03, CS04;
    private int Nbr_SceneD;
    private int Nbr_SceneB;
    public GameObject[] GO_Back;
    // public TextMesh Text;
    public Text TextDisplace;
    public Text TextBack;
    //public GameObject GO01;
    //public Material MatScreen01;
    //public RenderTexture RT_Flux01;
    void Start()
    {
        Clean();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // R.material.SetTexture("_MainTex", m_01);
        }
    }

    public void ChangeDisplace()
    {
        if (Nbr_SceneD == 1)
        {
            Nbr_SceneD++;
            QuadRender.compute_shader = CS02;
            TextDisplace.text = "SCENE_DISPLACE_02";
        }else if (Nbr_SceneD == 2){
            Nbr_SceneD++;
            QuadRender.compute_shader = CS03;
            TextDisplace.text = "SCENE_DISPLACE_03";
        }else if (Nbr_SceneD == 3){
            Nbr_SceneD++;
            QuadRender.compute_shader = CS04;
            TextDisplace.text = "SCENE_DISPLACE_04";
        }else if(Nbr_SceneD == 4){
            Nbr_SceneD = 1;
            TextDisplace.text = "SCENE_DISPLACE_01";
        }
    }

    public void ChangeBack()
    {

        if (Nbr_SceneD == 1){
            Nbr_SceneB++;
            GO_Back[0].SetActive(false);
            GO_Back[1].SetActive(true);
            VisualEffect VisualFX1 = GO_Back[1].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX1;
            S_Midi.MovableObject = GO_Back[1];
            TextBack.text = "SCENE_BACKGROUND_02";
        }
        else if (Nbr_SceneD == 2){
            Nbr_SceneB++;
            GO_Back[1].SetActive(false);
            GO_Back[2].SetActive(true);
            VisualEffect VisualFX2 = GO_Back[2].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX2;
            S_Midi.MovableObject = GO_Back[2];
            TextBack.text = "SCENE_BACKGROUND_03";
        }
        else if (Nbr_SceneD == 3){
            Nbr_SceneB++;
            GO_Back[2].SetActive(false);
            GO_Back[3].SetActive(true);
            VisualEffect VisualFX3 = GO_Back[3].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX3;
            S_Midi.MovableObject = GO_Back[3];
            TextBack.text = "SCENE_BACKGROUND_04";
        }
        else if (Nbr_SceneD == 4){
            Nbr_SceneB = 1;
            GO_Back[3].SetActive(false);
            GO_Back[0].SetActive(true);
            VisualEffect VisualFX0 = GO_Back[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX0;
            S_Midi.MovableObject = GO_Back[0];
            TextBack.text = "SCENE_BACKGROUND_01";
        }
    }

    void Clean()
    {
        VisualEffect VisualFX0 = GO_Back[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
        S_Midi.FX = VisualFX0;
        S_Midi.MovableObject = GO_Back[0];
        TextDisplace.text = "SCN_DISPLACE 01";
        TextBack.text = "SCN_BACK 01";
        Nbr_SceneB = 1;
        Nbr_SceneD = 1;
        QuadRender.compute_shader = CS_Default;
        //R.material.EnableKeyword("_Default");
        //R.material.SetTexture("_MainTex", m_Default);
       // R.material.EnableKeyword("_01");
        GO_Back[0].SetActive(true);
        GO_Back[1].SetActive(false);
        GO_Back[2].SetActive(false);
        GO_Back[3].SetActive(false);
    }


}
