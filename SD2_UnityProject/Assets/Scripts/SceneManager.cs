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
    public GameObject[] GO_Back;

    [Header("Managment Stuff")]
    public string Current;
    public string Next;
    public Animator AC;
    public Text TextDisplace;
    public Text TextBack;
    private int Nbr_SceneD;
    public int Nbr_SceneB;
    private int Nbr_TXT;
    private bool T =false;
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
        if (Nbr_TXT == 1)
        {
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT02Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT02NRM);
            Nbr_TXT++;
        }
        else if (Nbr_TXT == 2)
        {
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT03Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT03NRM);
            Nbr_TXT++;
        }
        else if (Nbr_TXT == 3)
        {
            RenderFinal.sharedMaterial.SetTexture("_Rought", TXT01Rought);
            RenderFinal.sharedMaterial.SetTexture("_NRM", TXT01NRM);
            Nbr_TXT = 1;
        }
    }
        public void ChangeDisplace()
        {
        if (Nbr_SceneD == 1)
        {        
            ScriptRender.compute_shader = Deform01;
        }else if (Nbr_SceneD == 2){
            ScriptRender.compute_shader = Deform02;
            Nbr_SceneD = 0;
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
        Nbr_SceneD++;
    }

    public void ChangeBack()
    {
        if (Nbr_SceneB == 1){
            GO_Back[0].SetActive(false);
            GO_Back[1].SetActive(true);
            VisualEffect VisualFX1 = GO_Back[1].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX1;
            S_Midi.MovableObject = GO_Back[1];
        }else if (Nbr_SceneB == 2){
            GO_Back[1].SetActive(false);
            GO_Back[2].SetActive(true);
            VisualEffect VisualFX2 = GO_Back[2].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX2;
            S_Midi.MovableObject = GO_Back[2];
        }else if (Nbr_SceneB == 3){
            GO_Back[2].SetActive(false);
            GO_Back[3].SetActive(true);
            //VisualEffect VisualFX3 = GO_Back[3].GetComponent(typeof(VisualEffect)) as VisualEffect;
            //S_Midi.FX = VisualFX3;
            //S_Midi.MovableObject = GO_Back[3];
        }else if (Nbr_SceneB == 4){
            GO_Back[3].SetActive(false);
            GO_Back[0].SetActive(true);
            VisualEffect VisualFX0 = GO_Back[0].GetComponent(typeof(VisualEffect)) as VisualEffect;
            S_Midi.FX = VisualFX0;
            S_Midi.MovableObject = GO_Back[0];
            Nbr_SceneB = 0;
        }
        Nbr_SceneB++;
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
        Nbr_TXT = 1;
        ScriptRender.compute_shader = CS_Deform;
        GO_Back[0].SetActive(true);
        GO_Back[1].SetActive(false);
        GO_Back[2].SetActive(false);
        GO_Back[3].SetActive(false);
        T = false;

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
    public void SetupParam3DshapeSoft()
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
        /* Render3Dshape.sharedMaterial.SetFloat("masque", 17.23f);
         Render3Dshape.sharedMaterial.SetFloat("focal", 1f);
         Render3Dshape.sharedMaterial.SetFloat("distance", 21.68f);
         Render3Dshape.sharedMaterial.SetFloat("rotateX", 0.1f);
         Render3Dshape.sharedMaterial.SetFloat("rotateY",0.02f);
         Render3Dshape.sharedMaterial.SetFloat("rotateY", 0.04f);
         Render3Dshape.sharedMaterial.SetFloat("smoothform", 1f);
         Render3Dshape.sharedMaterial.SetFloat("complexity", 1f);
         Render3Dshape.sharedMaterial.SetFloat("taille", 0.46f);*/
        Debug.Log("ok paarameter");
        //Render3Dshape.sharedMaterial.SetVector("_modifforme01", 1.5, 1.8, 1.3);
        // Render3Dshape.sharedMaterial("_modifforme01", 1.5, 1.8, 1.3, 0);
        //  Render3Dshape.sharedMaterial.SetVector4("_modifforme01", 1.5,1.8,1.3,0);
        //  Render3Dshape.sharedMaterial.SetFloat("_modifforme02", 0.7,0.6,0.4,0);
    }
    public void SetupParam3DshapeComplex()
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
       /* Render3Dshape.sharedMaterial.SetFloat("masque", 20f);
        Render3Dshape.sharedMaterial.SetFloat("focal", 1f);
        Render3Dshape.sharedMaterial.SetFloat("distance", 20f);
        Render3Dshape.sharedMaterial.SetFloat("rotateX", 0.1f);
        Render3Dshape.sharedMaterial.SetFloat("rotateY", 0.02f);
        Render3Dshape.sharedMaterial.SetFloat("rotateY", 0.04f);
        Render3Dshape.sharedMaterial.SetFloat("smoothform", 1f);
        Render3Dshape.sharedMaterial.SetFloat("complexity", 1f);
        Render3Dshape.sharedMaterial.SetFloat("taille", 0.45f);
        Debug.Log("ok parameter 3D shape complex");
        //  Render3Dshape.sharedMaterial.SetVector4("_modifforme01", 1.5,1.8,1.3,0);
        //  Render3Dshape.sharedMaterial.SetFloat("_modifforme02", 0.7,0.6,0.4,0);
    */
        }

    public void SetupParam3DshapePetit()
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
        /* Render3Dshape.sharedMaterial.SetFloat("masque", 20f);
         Render3Dshape.sharedMaterial.SetFloat("focal", 1f);
         Render3Dshape.sharedMaterial.SetFloat("distance", 20f);
         Render3Dshape.sharedMaterial.SetFloat("rotateX", 0.1f);
         Render3Dshape.sharedMaterial.SetFloat("rotateY", 0.02f);
         Render3Dshape.sharedMaterial.SetFloat("rotateY", 0.04f);
         Render3Dshape.sharedMaterial.SetFloat("smoothform", 1f);
         Render3Dshape.sharedMaterial.SetFloat("complexity", 1f);
         Render3Dshape.sharedMaterial.SetFloat("taille", 0.45f);
         Debug.Log("ok parameter 3D shape complex");
         //  Render3Dshape.sharedMaterial.SetVector4("_modifforme01", 1.5,1.8,1.3,0);
         //  Render3Dshape.sharedMaterial.SetFloat("_modifforme02", 0.7,0.6,0.4,0);
     */
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
                GO_Back[0].SetActive(true);
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
                GO_Back[0].SetActive(true);
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
                GO_Back[0].SetActive(true);
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
            GO_Back[0].SetActive(false);
            GO_Back[1].SetActive(false);
            GO_Back[2].SetActive(false);
            GO_Back[3].SetActive(false);
        }
    }

    public void Endtransition()
    {
        if (Next == "Nebula")
        {
            Current = "Nebula";
        }
        else if (Next == "Sunshaft")
        {
            Current = "Sunshaft";
        }
        else if (Next == "Cam")
        {
            Current = "Cam";
        }
        else if (Next == "FX")
        {
            Current = "FX";          
        }
        //RenderFinal.sharedMaterial.SetFloat("_Transition", 1);
    }

   public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Application.LoadLevel("SCN_Main");
    }

}
