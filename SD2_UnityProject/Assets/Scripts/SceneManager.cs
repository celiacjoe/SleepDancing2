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
    public UI_Manager S_UI;

    [Header("Final Render")]
    public Renderer RenderFinal;
    public render ScriptRender;
    public GameObject[] GO_FinalQuad;
    public Texture TXT01Rought, TXT01NRM, TXT02Rought, TXT02NRM, TXT03Rought, TXT03NRM;

    [Header("Displace Element")]
    public Renderer Render3Dshape;
    public ShapeRender S_ShapeRender;
    public ComputeShader CS_Deform, Deform01, Deform02;

    [Header("Back Element")]
    public GameObject[] FX_List;

    [Header("Managment Stuff")]
    public GameObject OSC_FX;
    public string Current;
    public string Next;
    public Animator AC;
    private int Nbr_Fluid;
    public int Nbr_FX;
    private int Nbr_Grain;
    //[Space(10)]
    void Start()
    {
        S_UI.T_FX = " Color";
        RenderFinal.sharedMaterial.SetFloat("_Transition", 1);
        Current = "Nebula";
        S_UI.UI_Current.GetComponentInChildren<Text>().text = "intro";
        Clean();
        RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 1);
        //SetupParam3Dshape();
    }

    void Update()
    {
        S_UI.UI_Current.GetComponentInChildren<Text>().text = "CURRENT/ " + Current;
        S_UI.UI_Next.GetComponentInChildren<Text>().text = "NEXT/ " + Next;
        S_UI.UI_FX.GetComponentInChildren<Text>().text = "FX /" + S_UI.T_FX;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetLevel();
            RenderFinal.sharedMaterial.SetFloat("_Transition", 1);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 1);
        }
    }
        public void ChangeGrainTexture()
        {
        if (Nbr_Grain == 1){
            S_UI.UI_Grain.GetComponentInChildren<Text>().text = "TXT GRAIN 01";
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT01Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT01NRM);
            RenderFinal.sharedMaterial.SetInt("TxtProcedural", 0);
            Nbr_Grain++;
        }
        else if (Nbr_Grain == 2){
            S_UI.UI_Grain.GetComponentInChildren<Text>().text = "TXT GRAIN 02";
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT02Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT02NRM);
            RenderFinal.sharedMaterial.SetInt("TxtProcedural", 0);
            Nbr_Grain++;
        }
        else if (Nbr_Grain == 3){
            S_UI.UI_Grain.GetComponentInChildren<Text>().text = "TXT PROCEDURAL";
            RenderFinal.sharedMaterial.SetInt("TxtProcedural", 1);
            //RenderFinal.sharedMaterial.SetTexture("_NRM", TXT01NRM);
            Nbr_Grain = 1;
        }
    }
        public void ChangeDisplace()
        {
        if (Nbr_Fluid == 1){
            S_UI.UI_Deform.GetComponentInChildren<Text>().text = "DEFORM/ Dendritic";
            ScriptRender.compute_shader = Deform01;
        }else if (Nbr_Fluid == 2){
            S_UI.UI_Deform.GetComponentInChildren<Text>().text = "DEFORM/ Fluid01";
            ScriptRender.compute_shader = Deform02;
            Nbr_Fluid = 1;
        }
        /*else if (Nbr_SceneD == 3){
            Nbr_SceneD++;
            ScriptRender.compute_shader = Deform03;
            TextDisplace.text = "SCENE_DISPLACE_04";
            TransitionScene();
        }
        /*else if(Nbr_SceneD == 3){
            Nbr_SceneD = 1;
            TextDisplace.text = "SCENE_DISPLACE_01";
        }*/
        Nbr_Fluid++;
    }

    public void ChangeFX()
    {
        if (Nbr_FX == 0){
            S_UI.T_FX = " Color";
            //S_UI.UI_FX.GetComponentInChildren<Text>().text = "FX COLOR";
            FX_List[0].SetActive(true);
            FX_List[1].SetActive(false);
            FX_List[2].SetActive(false);
            //FX_List[3].SetActive(false);
            VisualEffect VisualFX1 = FX_List[Nbr_FX].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX1;
            S_Midi.MovableObject = FX_List[Nbr_FX];
        }else if (Nbr_FX == 1){
            S_UI.T_FX = " Prism";
            //S_UI.UI_FX.GetComponentInChildren<Text>().text = "FX PRISM";
            FX_List[0].SetActive(false);
            FX_List[1].SetActive(true);
            FX_List[2].SetActive(false);
            //FX_List[3].SetActive(false);
            VisualEffect VisualFX2 = FX_List[Nbr_FX].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX2;
            S_Midi.MovableObject = FX_List[Nbr_FX];
        }else if (Nbr_FX == 2){
            S_UI.T_FX = " Gradient";
            //S_UI.UI_FX.GetComponentInChildren<Text>().text = "FX GRADIENT";
            FX_List[0].SetActive(false);
            FX_List[1].SetActive(false);
            FX_List[2].SetActive(true);
            //FX_List[3].SetActive(true);
            VisualEffect VisualFX3 = FX_List[Nbr_FX].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX3;
            S_Midi.MovableObject = FX_List[Nbr_FX];
        }

    }

    void Clean()
    {
        OSC_FX.SetActive(false);
        //FX
        VisualEffect VisualFX0 = FX_List[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
        S_Midi.FX = VisualFX0;
        S_Midi.MovableObject = FX_List[0];
        FX_List[Nbr_FX].SetActive(false);
        //Compteur Fx - fluide effect & TXT grain
        Nbr_FX = 0;
        Nbr_Fluid = 1;
        Nbr_Grain = 1;
        //Setup 1rst liquid
        ScriptRender.compute_shader = CS_Deform;
        // Setup Material transition
        RenderFinal.sharedMaterial.SetInt("_Nebula_Sunshaft", 0);
        RenderFinal.sharedMaterial.SetInt("_Nebula_Cam", 0);
        RenderFinal.sharedMaterial.SetInt("_Nebula_FX", 0);
        RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 1);
        RenderFinal.sharedMaterial.SetInt("_Sunshaft_Cam", 0);
        RenderFinal.sharedMaterial.SetInt("_Sunshaft_FX", 0);
        RenderFinal.sharedMaterial.SetInt("_Cam_Nebula", 0);
        RenderFinal.sharedMaterial.SetInt("_Cam_Sunshaft", 0);
        RenderFinal.sharedMaterial.SetInt("_Cam_FX", 0);
        RenderFinal.sharedMaterial.SetInt("_FX_Nebula", 0);
        RenderFinal.sharedMaterial.SetInt("_FX_Sunshaft", 0);
        RenderFinal.sharedMaterial.SetInt("_FX_Cam", 0);
    }
    public void Setting3Dshape01()
    {
        S_UI.UI_SettingShape.GetComponentInChildren<Text>().text = "SETTING SHAPE 01";
        S_ShapeRender.masque = 15f ;
        S_ShapeRender.focal= 1f;
        S_ShapeRender.distance = 14f;
        S_ShapeRender.rotateX = 0.1f;
        S_ShapeRender.rotateY = 0.02f;
        S_ShapeRender.rotateZ = 0.04f;
        S_ShapeRender.smoothForm = 0.8f;
        S_ShapeRender.complexity = 0.5f;
        S_ShapeRender.taille = 0.65f;       
    }
    public void Setting3Dshape02()
    {
        S_UI.UI_SettingShape.GetComponentInChildren<Text>().text = "SETTING SHAPE 02";
        S_ShapeRender.masque = 20f;
        S_ShapeRender.focal = 1f;
        S_ShapeRender.distance =20f;
        S_ShapeRender.rotateX = 0.1f;
        S_ShapeRender.rotateY = 0.02f;
        S_ShapeRender.rotateZ = 0.04f;
        S_ShapeRender.smoothForm = 1f;
        S_ShapeRender.complexity = 1f;
        S_ShapeRender.taille = 0.45f;       
    }
    public void Setting3Dshape03()
    {
        S_UI.UI_SettingShape.GetComponentInChildren<Text>().text = "SETTING SHAPE 03";
        S_ShapeRender.masque = 14.5f;
        S_ShapeRender.focal = 1f;
        S_ShapeRender.distance = 15f;
        S_ShapeRender.rotateX = 0.2f;
        S_ShapeRender.rotateY = 0.025f;
        S_ShapeRender.rotateZ = 0.025f;
        S_ShapeRender.smoothForm = 0.5f;
        S_ShapeRender.complexity = 0.5f;
        S_ShapeRender.taille = 0.45f;       
    }

    public void TransitionScene()
    {
        if (Current == "Nebula")
        {
            //S_UI.UI_FX.SetActive(false);
            if (Next == "Sunshaft")
            {
                GO_FinalQuad[1].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change nebula vers sunshaft");
            }
            else if (Next == "Cam")
            {
                GO_FinalQuad[2].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change nebula vers Cam");
            }
            else if (Next == "FX")
            {
                S_UI.UI_FX.SetActive(true);      
                FX_List[Nbr_FX].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change nebula vers FX");
            }
            else if (Next == "Volume")
            {
                GO_FinalQuad[3].SetActive(true);
                GO_FinalQuad[4].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + "FX", 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change nebula vers Volume");
            }
            AC.SetTrigger("Transition");
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 0);
            RenderFinal.sharedMaterial.SetInt("_FX_Nebula", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_Nebula", 0);
            GO_FinalQuad[0].SetActive(false);
            S_UI.UI_FX.SetActive(false);
        }
        else if (Current == "Sunshaft")
        {
            //S_UI.UI_FX.SetActive(false);
            if (Next == "Nebula")
            {
                GO_FinalQuad[0].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Sunshaft vers Nebula");
            }
            else if (Next == "Cam")
            {
                GO_FinalQuad[2].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Sunshaft vers cam");
            }
             else if (Next == "FX")
            {
                S_UI.UI_FX.SetActive(true);              
                FX_List[Nbr_FX].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Sunshaft vers FX");
            }else if (Next == "Volume")
            {
                GO_FinalQuad[3].SetActive(true);
                GO_FinalQuad[4].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + "FX", 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Sunshaft vers Volume");
            }
            AC.SetTrigger("Transition");
            RenderFinal.sharedMaterial.SetInt("_Nebula_Sunshaft", 0);
            RenderFinal.sharedMaterial.SetInt("_FX_Sunshaft", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_Sunshaft", 0);
            GO_FinalQuad[1].SetActive(false);
        }
        else if (Current == "Cam")
        {
            //S_UI.UI_FX.SetActive(false);
            if (Next == "Nebula")
            {
                GO_FinalQuad[0].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_"+ Current+"_"+ Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Cam vers Nebula");
            }
            else if (Next == "Sunshaft")
            {
                GO_FinalQuad[1].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change cam vers Sunshaft ");
            }
            else if (Next == "FX")
            {
                S_UI.UI_FX.SetActive(true);                
                FX_List[Nbr_FX].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Cam vers FX");
            }else if (Next == "Volume")
            {
                GO_FinalQuad[3].SetActive(true);
                GO_FinalQuad[4].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + "FX", 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Cam vers Volume");
            }
            AC.SetTrigger("Transition");
            GO_FinalQuad[2].SetActive(false);
            RenderFinal.sharedMaterial.SetInt("_FX_Cam", 0);
            RenderFinal.sharedMaterial.SetInt("_Nebula_Cam", 0);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_Cam", 0);
        }
        else if (Current == "FX"){
            S_UI.UI_FX.SetActive(true);
            if (Next == "Nebula")
            {
                GO_FinalQuad[0].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change FX vers Nebula");
            }
            else if (Next == "Sunshaft")
            {
                GO_FinalQuad[1].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change FX vers Sunshaft");
            }
            else if (Next == "Cam")
            {
                GO_FinalQuad[2].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change FX vers cam");
            }
            RenderFinal.sharedMaterial.SetInt("_Nebula_FX", 0);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_FX", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_FX", 0);
            if (Next == "Volume")
            {
                RenderFinal.sharedMaterial.SetInt("_Nebula_FX", 1);
                GO_FinalQuad[3].SetActive(true);
                GO_FinalQuad[4].SetActive(true);
                //RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                //RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change FX vers Volume");
            }
            AC.SetTrigger("Transition");
            //FX_List[Nbr_FX].SetActive(false);
        }else if (Current == "Volume")
        {
            if (Next == "Nebula")
            {
                GO_FinalQuad[0].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_FX_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Volume vers Nebula");
                AC.SetTrigger("Transition");
                RenderFinal.sharedMaterial.SetInt("Nebula_Volume", 0);
                RenderFinal.sharedMaterial.SetInt("Sunshaft_Volume", 0);
                RenderFinal.sharedMaterial.SetInt("Cam_Volume", 0);
            }
            else if (Next == "Sunshaft")
            {
                GO_FinalQuad[1].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_FX_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Volume vers Sunshaft");
                AC.SetTrigger("Transition");
                RenderFinal.sharedMaterial.SetInt("Nebula_Volume", 0);
                RenderFinal.sharedMaterial.SetInt("Sunshaft_Volume", 0);
                RenderFinal.sharedMaterial.SetInt("Cam_Volume", 0);
            }
            else if (Next == "Cam")
            {
                GO_FinalQuad[2].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_FX_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Volume vers cam");
                AC.SetTrigger("Transition");
            }
            RenderFinal.sharedMaterial.SetInt("_Nebula_FX", 0);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_FX", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_FX", 0);
            if (Next == "FX")
            {
                RenderFinal.sharedMaterial.SetInt("_Nebula_FX", 1);
                S_UI.UI_FX.SetActive(true);
                FX_List[Nbr_FX].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                GO_FinalQuad[4].SetActive(false);
                Debug.Log("Change Volume vers FX");
            }
            // GO_FinalQuad[4].SetActive(false);
            //FX_List[Nbr_FX].SetActive(false);
        }
    }
    public void Endtransition()
    {
        if (Next == "Nebula"){
            OSC_FX.SetActive(false);
            S_UI.UI_FX.SetActive(false);
            GO_FinalQuad[3].SetActive(false);
            FX_List[Nbr_FX].SetActive(false);
            Current = "Nebula";
            GO_FinalQuad[4].SetActive(false);
        }
        else if (Next == "Sunshaft"){
            OSC_FX.SetActive(false);
            GO_FinalQuad[3].SetActive(false);
            FX_List[Nbr_FX].SetActive(false);
            S_UI.UI_FX.SetActive(false);
            Current = "Sunshaft";
            GO_FinalQuad[4].SetActive(false);
        }
        else if (Next == "Cam"){
            OSC_FX.SetActive(true);
            GO_FinalQuad[3].SetActive(false);
            FX_List[Nbr_FX].SetActive(false);
            S_UI.UI_FX.SetActive(false);
            GO_FinalQuad[4].SetActive(false);
            Current = "Cam";
        }else if (Next == "FX"){
            OSC_FX.SetActive(true);
            S_UI.UI_FX.SetActive(true);
            Current = "FX";
            GO_FinalQuad[4].SetActive(false);
        }
        else if (Next == "Volume")
        {
            OSC_FX.SetActive(true);
            S_UI.UI_FX.SetActive(true);
            Current = "Volume";
        }
    }
   public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Application.LoadLevel("SCN_Main");
    }



}
