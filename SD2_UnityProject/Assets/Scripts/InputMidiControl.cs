using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class InputMidiControl : MonoBehaviour
{

    ///////////// VAR
    public render S_FinalRender;
    public VideoDeform S_VideoDeform;
    public Master_Control S_MasterControl;
    public SoundRender S_SunshaftRender;
    //public Camera Cam;
    public SceneManager Manager;
    public UI_Manager S_UI;
    public Renderer Mat_RenderFinal;
    public VisualEffect FX;
    public bool SoundControl01 = false;
    public bool SoundControl02 = false;
    public bool SoundControl03 = false;
    public float MultiplierSound01Value;
    public float MultiplierSound02Value;
    public float MultiplierSound03Value;
    private float BlurIntensityValue;
    public float RoughtIntensityValue;
    //public float RoughtIntensityDynamic;
    public float IntensityControlValue;
    public float AppFormeValue;
    private float TailleValue;
    private float TailleValue2;
    private float FormeValue;
    private float DisparitionValue;
    private float ZoomValue;
    public float SmoothT = 0.2f;
    private Vector3 velocity;
    private float PosX;
    private float PosY;
    public float FxP1Value;
    public float FxP2Value;
    public float FxP3Value;
    public float FxP4Value;
    public float MultiplierSoundValueFX;
    private bool Noir;
    public bool NoirCourt;
    private bool FXActive;
    public GameObject MovableObject;

    [Header("DEFORMATION CONTROLL")]
    [SerializeField] InputAction _BlurIntensity = null;
    [SerializeField] InputAction _RoughtIntensity = null;
    [SerializeField] InputAction _IntensityControl = null;
    [SerializeField] InputAction _ApparitionForme = null;
    [SerializeField] InputAction _Taille = null;
    [SerializeField] InputAction _Taille2 = null;
    [SerializeField] InputAction _Forme = null;
    [SerializeField] InputAction _Disparition = null;
    [SerializeField] InputAction _Zoom = null;
    [Header("FX CONTROLL")]
    [SerializeField] InputAction _PositionX = null;
    [SerializeField] InputAction _PositionY = null;
    [SerializeField] InputAction _FXParam1 = null;
    public string Name_P1;
    [SerializeField] InputAction _FXParam2 = null;
    public string Name_P2;
    [SerializeField] InputAction _FXParam3 = null;
    public string Name_P3;
    [SerializeField] InputAction _FXParam4 = null;
    public string Name_P4;
    [SerializeField] InputAction _VolumeParam01 = null;
    [SerializeField] InputAction _VolumeParam02 = null;
    [Header("SCENE CONTROLL")]
    [SerializeField] InputAction _FadeNoir = null;
    [SerializeField] InputAction _ScnNebula = null;
    [SerializeField] InputAction _ScnSunshaft = null;
    [SerializeField] InputAction _ScnCam = null;
    [SerializeField] InputAction _ScnFX = null;
    [SerializeField] InputAction _ScnVolume = null;
    [SerializeField] InputAction _ScnDendritic = null;
    [Header("NEXT")]
    [SerializeField] InputAction _ChangeFluid = null;
    [SerializeField] InputAction _ActiveFX = null;
    [SerializeField] InputAction _ChangeFX01 = null;
    [SerializeField] InputAction _ChangeFX02 = null;
    [SerializeField] InputAction _ChangeFX03 = null;
    [SerializeField] InputAction _ChangeGrain = null;
    [Header("PRESET 3D SHAPE ASSIGNMENT")]
    [SerializeField] InputAction _Setting3Dshape01 = null;
    [SerializeField] InputAction _Setting3Dshape02 = null;
    [SerializeField] InputAction _Setting3Dshape03 = null;
    [Header("SOUND CONTROL")]
    [SerializeField] InputAction _ActiveSoundControl01 = null;
    [SerializeField] InputAction _ActiveSoundControl02 = null;
    [SerializeField] InputAction _ActiveSoundControl03 = null;
    [SerializeField] InputAction _MultiplierSound01 = null;
    [SerializeField] InputAction _MultiplierSound02 = null;
    [SerializeField] InputAction _MultiplierSound03 = null;
    [SerializeField] InputAction _MultiplierSoundFX = null;

    ///////////// FUNCTION
    float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
    {
        return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
    }
    void Start()
    {
        FXActive = false;
        Noir = false;
        NoirCourt = false;
    }
    void Update()
    {
        Mat_RenderFinal.sharedMaterial.SetFloat("BlurIntensity", S_MasterControl.SmoothBlur);
        Mat_RenderFinal.sharedMaterial.SetFloat("Intensity", S_MasterControl.SmoothIntensity);
        Mat_RenderFinal.sharedMaterial.SetFloat("_ApparitionForme", S_MasterControl.SmoothAppForme);
        S_FinalRender.ApparitionForme = S_MasterControl.SmoothAppForme;
        S_FinalRender.Taille = S_MasterControl.SmoothTaille;
        S_FinalRender.Forme = S_MasterControl.SmoothForme;
        S_FinalRender.Disparition = S_MasterControl.SmoothDisparition;
        //Mat_RenderFinal.sharedMaterial.SetFloat("Taille", S_MasterControl.SmoothTaille);
        Mat_RenderFinal.sharedMaterial.SetFloat("Forme", S_MasterControl.SmoothForme);
        Mat_RenderFinal.sharedMaterial.SetFloat("Disparition", S_MasterControl.SmoothDisparition);

        Vector3 NewTargetPosition = new Vector3(PosX+40,-4.5f);
        MovableObject.transform.position = Vector3.SmoothDamp(MovableObject.transform.position, NewTargetPosition, ref velocity, SmoothT);
        //MovableObject.transform.position.x = f.SmoothDamp(MovableObject.transform.position.x, NewTargetPosition.x, ref velocity, SmoothT);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Manager.ResetLevel();
            Manager.RenderFinal.sharedMaterial.SetFloat("_Transition", 1);
            Manager.RenderFinal.sharedMaterial.SetInt("_Sunshaft_Nebula", 1);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Manager.Next = "Nebula";
            Manager.TransitionScene();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Manager.Next = "Sunshaft";
            Manager.TransitionScene();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Manager.Next = "Cam";
            Manager.TransitionScene();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Manager.Next = "FX";
            Manager.TransitionScene();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Manager.Next = "Volume";
            Manager.TransitionScene();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Manager.Next = "Dendritic";
            Manager.TransitionScene();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Manager.AC.GetBool("FadeNoir");
            if (!Noir)
            {
                S_UI.UI_Noir.SetActive(true);
                S_UI.UI_Noir.GetComponentInChildren<Text>().text = "NOIR LONG INCOMING";
                Manager.AC.SetBool("FadeNoir", true);
                Noir = true;
            }
            else
            {
                S_UI.UI_Noir.SetActive(false);
                Manager.AC.SetBool("FadeNoir", false);
                Noir = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Manager.AC.GetBool("FadeNoirCourt");
            if (!NoirCourt)
            {
                S_UI.UI_Noir.SetActive(true);
                S_UI.UI_Noir.GetComponentInChildren<Text>().text = "NOIR SHORT INCOMONG";
                Manager.AC.SetBool("FadeNoirCourt", true);
                NoirCourt = true;
            }
            else
            {
                S_UI.UI_Noir.SetActive(false);
                Manager.AC.SetBool("FadeNoirCourt", false);
                NoirCourt = false;
            }
        }

    }
    void OnEnable()
    {
        _BlurIntensity.performed += BlurIntensity;
        _BlurIntensity.Enable();

        _ApparitionForme.performed += ApparitionForme;
        _ApparitionForme.Enable();

        _Taille.performed += Taille;
        _Taille.Enable();

        _Taille2.performed += Taille2;
        _Taille2.Enable();

        _Forme.performed += Forme;
        _Forme.Enable();

        _RoughtIntensity.performed += RoughtIntensity;
        _RoughtIntensity.Enable();

        _Disparition.performed += Disparition;
        _Disparition.Enable();

        _IntensityControl.performed += IntensityControl;
        _IntensityControl.Enable();

        _Zoom.performed += Zoom;
        _Zoom.Enable();

        _PositionX.performed += PositionX;
        _PositionX.Enable();

        _PositionY.performed += PositionY;
        _PositionY.Enable();

        _FXParam1.performed += FXParam1;
        _FXParam1.Enable();

        _FXParam2.performed += FXParam2;
        _FXParam2.Enable();

        _FXParam3.performed += FXParam3;
        _FXParam3.Enable();

        _FXParam4.performed += FXParam4;
        _FXParam4.Enable();

        _VolumeParam01.performed += VolumeParam01;
        _VolumeParam01.Enable();

        _VolumeParam02.performed += VolumeParam02;
        _VolumeParam02.Enable();

        _ChangeFluid.performed += ChangeFluid;
        _ChangeFluid.Enable();

        _ActiveFX.performed += ActiveFX;
        _ActiveFX.Enable();

        _ChangeFX01.performed += ChangeFX01;
        _ChangeFX01.Enable();

        _ChangeFX02.performed += ChangeFX02;
        _ChangeFX02.Enable();

        _ChangeFX03.performed += ChangeFX03;
        _ChangeFX03.Enable();

        _ChangeGrain.performed += ChangeGrain;
        _ChangeGrain.Enable();

        _FadeNoir.performed += FadeNoir;
        _FadeNoir.Enable();

        _ScnNebula.performed += ScnNebula;
        _ScnNebula.Enable();

        _ScnSunshaft.performed += ScnSunshaft;
        _ScnSunshaft.Enable();

        _ScnCam.performed += ScnCam;
        _ScnCam.Enable();

        _ScnFX.performed += ScnFX;
        _ScnFX.Enable();

        _ScnVolume.performed += ScnVolume;
        _ScnVolume.Enable();

        _ScnDendritic.performed += ScnDendritic;
        _ScnDendritic.Enable();

        _Setting3Dshape01.performed += Set3DShapeSoft;
        _Setting3Dshape01.Enable();

        _Setting3Dshape02.performed += Set3DShapeComplex;
        _Setting3Dshape02.Enable();

        _Setting3Dshape03.performed += Set3DShapePetit;
        _Setting3Dshape03.Enable();

        _ActiveSoundControl01.performed += ActiveSoundControl01;
        _ActiveSoundControl01.Enable();

        _ActiveSoundControl02.performed += ActiveSoundControl02;
        _ActiveSoundControl02.Enable();

        _ActiveSoundControl03.performed += ActiveSoundControl03;
        _ActiveSoundControl03.Enable();

        _MultiplierSound01.performed += MultiplierSound01;
        _MultiplierSound01.Enable();

        _MultiplierSound02.performed += MultiplierSound02;
        _MultiplierSound02.Enable();

        _MultiplierSound03.performed += MultiplierSound03;
        _MultiplierSound03.Enable();

        _MultiplierSoundFX.performed += MultiplierSoundFX;
        _MultiplierSoundFX.Enable();
    }

    void OnDisable()
    {
        _BlurIntensity.performed -= BlurIntensity;
        _BlurIntensity.Disable();

        _ApparitionForme.performed -= ApparitionForme;
        _ApparitionForme.Disable();

        _IntensityControl.performed -= IntensityControl;
        _IntensityControl.Disable();

        _Taille.performed -= Taille;
        _Taille.Disable();

        _Taille2.performed -= Taille2;
        _Taille2.Disable();

        _Forme.performed -= Forme;
        _Forme.Disable();

        _RoughtIntensity.performed -= RoughtIntensity;
        _RoughtIntensity.Disable();

        _Disparition.performed -= Disparition;
        _Disparition.Disable();

        _ChangeFluid.performed -= ChangeFluid;
        _ChangeFluid.Disable();

        _ActiveFX.performed -= ActiveFX;
        _ActiveFX.Disable();

        _ChangeFX01.performed -= ChangeFX01;
        _ChangeFX01.Disable();

        _ChangeFX02.performed -= ChangeFX02;
        _ChangeFX02.Disable();

        _ChangeFX03.performed -= ChangeFX03;
        _ChangeFX03.Disable();

        _ChangeGrain.performed -= ChangeGrain;
        _ChangeGrain.Disable();

        _Zoom.performed -= Zoom;
        _Zoom.Disable();

        _PositionX.performed -= PositionX;
        _PositionX.Disable();

        _PositionY.performed -= PositionY;
        _PositionY.Disable();

        _FXParam1.performed -= FXParam1;
        _FXParam1.Disable();

        _FXParam2.performed -= FXParam2;
        _FXParam2.Disable();

        _FXParam3.performed -= FXParam3;
        _FXParam3.Disable();

        _FXParam4.performed -= FXParam4;
        _FXParam4.Disable();

        _VolumeParam01.performed -= VolumeParam01;
        _VolumeParam01.Disable();

        _VolumeParam02.performed -= VolumeParam02;
        _VolumeParam02.Disable();

        _FadeNoir.performed -= FadeNoir;
        _FadeNoir.Disable();

        _ScnNebula.performed -= ScnNebula;
        _ScnNebula.Disable();

        _ScnSunshaft.performed -= ScnSunshaft;
        _ScnSunshaft.Disable();

        _ScnCam.performed -= ScnCam;
        _ScnCam.Disable();

        _ScnFX.performed -= ScnFX;
        _ScnFX.Disable();

        _ScnVolume.performed -= ScnVolume;
        _ScnVolume.Disable();

        _ScnDendritic.performed -= ScnDendritic;
        _ScnDendritic.Disable();

        _Setting3Dshape01.performed -= Set3DShapeSoft;
        _Setting3Dshape01.Disable();

        _Setting3Dshape02.performed -= Set3DShapeComplex;
        _Setting3Dshape02.Disable();

        _Setting3Dshape03.performed -= Set3DShapePetit;
        _Setting3Dshape03.Disable();

        _ActiveSoundControl01.performed -= ActiveSoundControl01;
        _ActiveSoundControl01.Disable();

        _ActiveSoundControl02.performed -= ActiveSoundControl02;
        _ActiveSoundControl02.Disable();

        _ActiveSoundControl03.performed -= ActiveSoundControl03;
        _ActiveSoundControl03.Disable();

        _MultiplierSound01.performed -= MultiplierSound01;
        _MultiplierSound01.Disable();

        _MultiplierSound02.performed -= MultiplierSound02;
        _MultiplierSound02.Disable();

        _MultiplierSound03.performed -= MultiplierSound03;
        _MultiplierSound03.Disable();

        _MultiplierSoundFX.performed -= MultiplierSoundFX;
        _MultiplierSoundFX.Disable();
    }
    void BlurIntensity(InputAction.CallbackContext ctx)
    {
        S_MasterControl.SharedBlurValue = ctx.ReadValue<float>();
       // BlurIntensityValue = ctx.ReadValue<float>();
    }
    void RoughtIntensity(InputAction.CallbackContext ctx)
    {
        RoughtIntensityValue = ctx.ReadValue<float>();
        if (!SoundControl01){
            Mat_RenderFinal.sharedMaterial.SetFloat("RoughtIntensity", RoughtIntensityValue);
            S_SunshaftRender.RoughtIntensity = RoughtIntensityValue;
            S_VideoDeform.RoughtIntensity = RoughtIntensityValue;
        }      
    }
    void IntensityControl(InputAction.CallbackContext ctx)
    {
        if (!SoundControl02)
        {
            S_MasterControl.SharedIntensityValue = ctx.ReadValue<float>();
         } 
       // S_MasterControl.SharedIntensityValue = IntensityControlValue;
       //Mat_RenderFinal.sharedMaterial.SetFloat("Intensity", IntensityControlValue);
    }
    void ApparitionForme(InputAction.CallbackContext ctx)
    {
        if (!SoundControl03)
        {
            S_MasterControl.SharedAppFormeValue = ctx.ReadValue<float>();
        }
        //AppFormeValue = ctx.ReadValue<float>();
      //  S_FinalRender.ApparitionForme = AppFormeValue;
      //  Mat_RenderFinal.sharedMaterial.SetFloat("_ApparitionForme", AppFormeValue);
    }
     void Taille(InputAction.CallbackContext ctx)
     {
        S_MasterControl.SharedTailleValue = ctx.ReadValue<float>();
        //TailleValue = ctx.ReadValue<float>();
        // S_FinalRender.Taille = TailleValue;
     }
    void Forme(InputAction.CallbackContext ctx)
    {
        S_MasterControl.SharedFormeValue = ctx.ReadValue<float>();
        //FormeValue = ctx.ReadValue<float>();
       // S_FinalRender.Forme = FormeValue;
    }
    void Disparition(InputAction.CallbackContext ctx)
    {
        S_MasterControl.SharedDisparitionValue = ctx.ReadValue<float>();
        // DisparitionValue = ctx.ReadValue<float>();
       // S_FinalRender.Disparition = DisparitionValue;
    }
    void Taille2(InputAction.CallbackContext ctx)
    {
        TailleValue2 = ctx.ReadValue<float>();
        S_FinalRender.Taille2 = TailleValue2;
    }
    void Zoom(InputAction.CallbackContext ctx)
     {
         ZoomValue = ctx.ReadValue<float>();
        // Cam.orthographicSize = map(ZoomValue,0, 1, 0.25f, 4.5f);
     }
    void PositionX(InputAction.CallbackContext ctx)
    {
        PosX = ctx.ReadValue<float>();
        PosX = map(PosX, 0, 1, -9f, 9f);
    }
    void PositionY(InputAction.CallbackContext ctx)
    {
        PosY = ctx.ReadValue<float>();
        PosY = map(PosY, 0, 1, -7f, 6f);
    }
    void FXParam1(InputAction.CallbackContext ctx)
    {
        FxP1Value = ctx.ReadValue<float>() ;
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P1, FxP1Value);
    }
    void FXParam2(InputAction.CallbackContext ctx)
    {
        FxP2Value = ctx.ReadValue<float>();
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P2, FxP2Value);
    }
    void FXParam3(InputAction.CallbackContext ctx)
    {
        FxP3Value = ctx.ReadValue<float>();
        if (SoundControl02 == true)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat(Name_P3, FxP3Value);
        }
    }
    void FXParam4(InputAction.CallbackContext ctx)
    {
        FxP4Value = ctx.ReadValue<float>();
        if (!SoundControl02)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat(Name_P4, FxP4Value);
        }
    }
    void VolumeParam01(InputAction.CallbackContext ctx)
    {
        float VolumParamValue01 = ctx.ReadValue<float>();
        //VisualEffect VFX = FX.GetComponent<VisualEffect>();
        S_VideoDeform.Forme= VolumParamValue01;
      //  VFX.SetFloat(Name_P5, VolumParamValue01);
    }
    void VolumeParam02(InputAction.CallbackContext ctx)
    {
        float VolumParamValue02 = ctx.ReadValue<float>();
        S_VideoDeform.Disparition = VolumParamValue02;
    }
    void ChangeFluid(InputAction.CallbackContext ctx)
    {
        Manager.ChangeDisplace();
    }
    void ActiveFX(InputAction.CallbackContext ctx)
    {
        if (!FXActive)
        {
            FXActive = true;
            Manager.FX_List[Manager.Nbr_FX].SetActive(true);
        }
        else
        {
            Manager.FX_List[Manager.Nbr_FX].SetActive(false);
            FXActive = false;
        }
    }
    void ChangeFX01(InputAction.CallbackContext ctx)
    {
        Manager.Nbr_FX = 0;
        Manager.ChangeFX();
    }
    void ChangeFX02(InputAction.CallbackContext ctx)
    {
        Manager.Nbr_FX = 1;
        Manager.ChangeFX();
    }
    void ChangeFX03(InputAction.CallbackContext ctx)
    {
        Manager.Nbr_FX = 2;
        Manager.ChangeFX();
    }
    void ChangeGrain(InputAction.CallbackContext ctx)
    {
        Manager.ChangeGrainTexture();
    }
    void FadeNoir(InputAction.CallbackContext ctx)
    {       
        Manager.AC.GetBool("FadeNoir");
        if (!Noir)
        {
            S_UI.UI_Noir.SetActive(true);
            S_UI.UI_Noir.GetComponentInChildren<Text>().text = "NOIR INCOMING";
            Manager.AC.SetBool("FadeNoir",true);
            Noir = true;
        }else
        {
            S_UI.UI_Noir.SetActive(false);
            Manager.AC.SetBool("FadeNoir", false);
            Noir = false ;
        }
    }
    void ScnNebula(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Nebula";
        Manager.TransitionScene();
    }
    void ScnSunshaft(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Sunshaft";
        Manager.TransitionScene();
    }
    void ScnCam(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Cam";
        Manager.TransitionScene();
    }
    void ScnFX(InputAction.CallbackContext ctx)
    {
        Manager.Next = "FX";
        Manager.TransitionScene();
    }
    void ScnVolume(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Volume";
        Manager.TransitionScene();
    }
    void ScnDendritic(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Dendritic";
        Manager.TransitionScene();
    }
    void Set3DShapeSoft(InputAction.CallbackContext ctx)
    {
        Manager.Setting3Dshape01();
    }
    void Set3DShapeComplex(InputAction.CallbackContext ctx)
    {
        Manager.Setting3Dshape02();
    }

    void Set3DShapePetit(InputAction.CallbackContext ctx)
    {
        Manager.Setting3Dshape03();
    }

    void ActiveSoundControl01(InputAction.CallbackContext ctx)
    {     
        if (SoundControl01){
            S_UI.UI_SoundControl01.SetActive(false);
            SoundControl01 = false;
        }else{
            S_UI.UI_SoundControl01.SetActive(true);
            SoundControl01 = true;
        }
    }
    void ActiveSoundControl02(InputAction.CallbackContext ctx)
    {
        if (SoundControl02){
            SoundControl02 = false;
            S_UI.UI_SoundControl02.SetActive(false);
        }else{
            SoundControl02 = true;
            S_UI.UI_SoundControl02.SetActive(true);
        }
    }
    void ActiveSoundControl03(InputAction.CallbackContext ctx)
    {
        if (SoundControl03){
            SoundControl03 = false;
            S_UI.UI_SoundControl03.SetActive(false);
        } else{
            SoundControl03 = true;
            S_UI.UI_SoundControl03.SetActive(true);
        }
    }
    void MultiplierSound01(InputAction.CallbackContext ctx)
    {
        MultiplierSound01Value = ctx.ReadValue<float>();
    }

    void MultiplierSound02(InputAction.CallbackContext ctx)
    {
        MultiplierSound02Value = ctx.ReadValue<float>();
    }

    void MultiplierSound03(InputAction.CallbackContext ctx)
    {
        MultiplierSound03Value = ctx.ReadValue<float>();
    }

    void MultiplierSoundFX(InputAction.CallbackContext ctx)
    {
        MultiplierSoundValueFX = ctx.ReadValue<float>();
    }

    /*  void Subdivision1(InputAction.CallbackContext ctx)
      {
          if (!Sequence.Subdivision1){
              UI_Subdiv.SetActive(true);
              Sequence.Subdivision1 = true;
          }else{
              UI_Subdiv.SetActive(false);
              Sequence.Subdivision1 = false;
          }       
      }
    */

}




