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
    public Texture TXT01Rought, TXT01NRM, TXT02Rought, TXT02NRM, TXT03Rought, TXT03NRM;

    [Header("Displace Element")]
    public Renderer Render3Dshape;
    public ShapeRender S_ShapeRender;
    public ComputeShader CS_Deform, Deform01, Deform02;

    [Header("Back Element")]
    public GameObject[] FX_List;

    [Header("Managment Stuff")]
    public string Current;
    public string Next;
    public Animator AC;
    private int Nbr_Fluid;
    public int Nbr_FX;
    private int Nbr_Grain;
    //[Space(10)]
    void Start()
    {
        RenderFinal.sharedMaterial.SetFloat("_Transition", 1);
        Current = "Nebula";
        Clean();
        RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 1);
        //SetupParam3Dshape();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetLevel();
            RenderFinal.sharedMaterial.SetFloat("_Transition", 1);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 1);
        }
    }
        public void ChangeGrainTexture()
        {
        if (Nbr_Grain == 1)
        {
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT02Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT02NRM);
            Nbr_Grain++;
        }
        else if (Nbr_Grain == 2)
        {
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT03Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT03NRM);
            Nbr_Grain++;
        }
        else if (Nbr_Grain == 3)
        {
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT01Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT01NRM);
            Nbr_Grain = 1;
        }
    }
        public void ChangeDisplace()
        {
        if (Nbr_Fluid == 1)
        {        
            ScriptRender.compute_shader = Deform01;
        }else if (Nbr_Fluid == 2){
            ScriptRender.compute_shader = Deform02;
            Nbr_Fluid = 0;
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
        if (Nbr_FX == 1){
            FX_List[0].SetActive(false);
            FX_List[1].SetActive(true);
            FX_List[2].SetActive(false);
            //FX_List[3].SetActive(false);
            VisualEffect VisualFX1 = FX_List[1].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX1;
            S_Midi.MovableObject = FX_List[1];
        }else if (Nbr_FX == 2){
            FX_List[0].SetActive(false);
            FX_List[1].SetActive(false);
            FX_List[2].SetActive(true);
            //FX_List[3].SetActive(false);
            VisualEffect VisualFX2 = FX_List[2].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX2;
            S_Midi.MovableObject = FX_List[2];
        }else if (Nbr_FX == 3){
            FX_List[0].SetActive(false);
            FX_List[1].SetActive(false);
            FX_List[2].SetActive(false);
            //FX_List[3].SetActive(true);
            //VisualEffect VisualFX3 = GO_Back[3].GetComponent(typeof(VisualEffect)) as VisualEffect;
            //S_Midi.FX = VisualFX3;
            //S_Midi.MovableObject = GO_Back[3];
        }else if (Nbr_FX == 4){
            FX_List[3].SetActive(false);
            FX_List[0].SetActive(true);
            VisualEffect VisualFX0 = FX_List[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX0;
            S_Midi.MovableObject = FX_List[0];
            Nbr_FX = 0;
        }
    }

    void Clean()
    {
        //FX
        VisualEffect VisualFX0 = FX_List[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
        S_Midi.FX = VisualFX0;
        S_Midi.MovableObject = FX_List[0];
        FX_List[0].SetActive(true);
        FX_List[1].SetActive(false);
        FX_List[2].SetActive(false);
        //FX_List[3].SetActive(false);
        //Compteur Fx - fluide effect & TXT grain
        Nbr_FX = 1;
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
                FX_List[0].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change nebula vers FX");
            }
            AC.SetTrigger("Transition");
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 0);
            RenderFinal.sharedMaterial.SetInt("_FX_Nebula", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_Nebula", 0);
            GO_FinalQuad[0].SetActive(false);

        }else if (Current == "Sunshaft")
        {
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
                FX_List[0].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Sunshaft vers FX");
            }
            AC.SetTrigger("Transition");
            RenderFinal.sharedMaterial.SetInt("_Nebula_Sunshaft", 0);
            RenderFinal.sharedMaterial.SetInt("_FX_Sunshaft", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_Sunshaft", 0);
            GO_FinalQuad[1].SetActive(false);

        }else if (Current == "Cam")
        {
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
                FX_List[0].SetActive(true);
                GO_FinalQuad[3].SetActive(true);
                RenderFinal.sharedMaterial.SetInt("_" + Current + "_" + Next, 1);
                RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
                Debug.Log("Change Cam vers FX");
            }
            AC.SetTrigger("Transition");
            GO_FinalQuad[2].SetActive(false);
            RenderFinal.sharedMaterial.SetInt("_FX_Cam", 0);
            RenderFinal.sharedMaterial.SetInt("_Nebula_Cam", 0);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_Cam", 0);
        }
        else if (Current == "FX"){
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
            AC.SetTrigger("Transition");
            //RenderFinal.sharedMaterial.SetFloat("_Transition", 0);
            RenderFinal.sharedMaterial.SetInt("_Nebula_FX", 0);
            RenderFinal.sharedMaterial.SetInt("_Sunshaft_FX", 0);
            RenderFinal.sharedMaterial.SetInt("_Cam_FX", 0);
            GO_FinalQuad[3].SetActive(false);
            FX_List[0].SetActive(false);
            FX_List[1].SetActive(false);
            FX_List[2].SetActive(false);
            FX_List[3].SetActive(false);
        }
    }

    public void Endtransition()
    {
        if (Next == "Nebula"){
            Current = "Nebula";
        }else if (Next == "Sunshaft"){
            Current = "Sunshaft";
        }else if (Next == "Cam"){
            Current = "Cam";
        }else if (Next == "FX"){
            Current = "FX";          
        }
    }

   public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Application.LoadLevel("SCN_Main");
    }

}
