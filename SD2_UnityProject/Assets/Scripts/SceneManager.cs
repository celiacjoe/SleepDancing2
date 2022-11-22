using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class SceneManager : MonoBehaviour
{

    [Header("Dependency")]
    public InputMidiControl S_Midi;
    public Timer S_Timer;

    [Header("Final Render")]
    public Renderer RenderFinal;
    public render ScriptRender;
    public GameObject[] GO_FinalQuad;

    [Header("Displace Element")]
    public Renderer Render3Dshape;
    public ComputeShader CS_Default, Deform01, Deform02, Deform03;

    [Header("Back Element")]
    public GameObject[] GO_Back;

    [Header("Managment Stuff")]
    public string SceneName;
    public Animator AC;
    public Text TextDisplace;
    public Text TextBack;
    private int Nbr_SceneD;
    private int Nbr_SceneB;
    private bool T =false;

    //[Space(10)]
    void Start()
    {

        SceneName = "Intro";
        Clean();
        //SetupParam3Dshape();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransitionScene();
            // R.material.SetTexture("_MainTex", m_01);
        }
        if (S_Timer.TimerDown == true && SceneName=="Intro")
        {
           // AC.SetTrigger("Nebula");
            TransitionScene();
        }
       // else if(SceneName == "Intro")
    }

    public void Scene01()
    {
        GO_Back[0].SetActive(true);
        S_Timer.StartTimer();
    }

        public void ChangeDisplace()
        {
        if (Nbr_SceneD == 1)
        {
            TextDisplace.text = "SCENE_DISPLACE_02";
            Nbr_SceneD++;
            ScriptRender.compute_shader = Deform01;
            TransitionScene();
        }else if (Nbr_SceneD == 2){
            TextDisplace.text = "SCENE_DISPLACE_03";
            Nbr_SceneD++;
            ScriptRender.compute_shader = Deform02;
            TransitionScene();
        }
        else if (Nbr_SceneD == 3){
            Nbr_SceneD++;
            ScriptRender.compute_shader = Deform03;
            TextDisplace.text = "SCENE_DISPLACE_04";
            TransitionScene();
        }
        else if(Nbr_SceneD == 4){
            Nbr_SceneD = 1;
            TextDisplace.text = "SCENE_DISPLACE_01";
            TransitionScene();
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
            TextBack.text = "SCENE_BACK_02";
        }
        else if (Nbr_SceneD == 2){
            Nbr_SceneB++;
            GO_Back[1].SetActive(false);
            GO_Back[2].SetActive(true);
            VisualEffect VisualFX2 = GO_Back[2].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX2;
            S_Midi.MovableObject = GO_Back[2];
            TextBack.text = "SCENE_BACK_03";
        }
        else if (Nbr_SceneD == 3){
            Nbr_SceneB++;
            GO_Back[2].SetActive(false);
            GO_Back[3].SetActive(true);
            VisualEffect VisualFX3 = GO_Back[3].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX3;
            S_Midi.MovableObject = GO_Back[3];
            TextBack.text = "SCENE_BACK_04";
        }
        else if (Nbr_SceneD == 4){
            Nbr_SceneB = 1;
            GO_Back[3].SetActive(false);
            GO_Back[0].SetActive(true);
            VisualEffect VisualFX0 = GO_Back[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX0;
            S_Midi.MovableObject = GO_Back[0];
            TextBack.text = "SCENE_BACK_01";
        }
    }

    void Clean()
    {
        SceneName = "intro";
        VisualEffect VisualFX0 = GO_Back[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
        S_Midi.FX = VisualFX0;
        S_Midi.MovableObject = GO_Back[0];
        TextDisplace.text = "SCN_DISPLACE 01";
        TextBack.text = "SCN_BACK 01";
        Nbr_SceneB = 1;
        Nbr_SceneD = 1;
        ScriptRender.compute_shader = CS_Default;
        //R.material.EnableKeyword("_Default");
        //R.material.SetTexture("_MainTex", m_Default);
       // R.material.EnableKeyword("_01");
        GO_Back[0].SetActive(true);
        GO_Back[1].SetActive(false);
        GO_Back[2].SetActive(false);
        GO_Back[3].SetActive(false);
        T = false;

    }

    void SetupParam3Dshape()
    {
        Render3Dshape.sharedMaterial.SetFloat("_masque", 17.23f);
        Render3Dshape.sharedMaterial.SetFloat("_focal", 1f);
        Render3Dshape.sharedMaterial.SetFloat("_distance", 21.68f);
        Render3Dshape.sharedMaterial.SetFloat("_rotateX", 0.1f);
        Render3Dshape.sharedMaterial.SetFloat("_rotateY",0.02f);
        Render3Dshape.sharedMaterial.SetFloat("_rotateY", 0.04f);
        Render3Dshape.sharedMaterial.SetFloat("_smoothform", 1f);
        Render3Dshape.sharedMaterial.SetFloat("_complexity", 1f);
        Render3Dshape.sharedMaterial.SetFloat("_taille", 0.46f);
      //  Render3Dshape.sharedMaterial.SetVector4("_modifforme01", 1.5,1.8,1.3,0);
      //  Render3Dshape.sharedMaterial.SetFloat("_modifforme02", 0.7,0.6,0.4,0);
    }

    public void TransitionScene()
    {
        if (RenderFinal.sharedMaterial.GetInt("_SunShaft_Nebula") == 1)
        {
            AC.SetTrigger("Transition");
            RenderFinal.sharedMaterial.SetInt("_SunShaft_Nebula", 0);
        }
        else if (RenderFinal.sharedMaterial.GetInt("_Nebula_SunShaft") == 1)
        {
            AC.SetTrigger("Transition");
            RenderFinal.sharedMaterial.SetInt("_Nebula_SunShaft", 0);
        }

    }


}
