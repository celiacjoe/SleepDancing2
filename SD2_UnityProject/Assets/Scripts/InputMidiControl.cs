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
    //public Camera Cam;
    public SceneManager Manager;
    public Renderer Mat_RenderFinal;
    public VisualEffect FX;
    public bool SoundControl01 = false;
    public bool SoundControl02 = false;
    public bool SoundControl03 = false;
    private float BlurIntensityValue;
    public float RoughtIntensityValue;
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
    private bool Noir;
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
    [SerializeField] InputAction _FXParam5 = null;
    public string Name_P5;
    [SerializeField] InputAction _FXParam6 = null;
    public string Name_P6;
    [Header("SCENE CONTROLL")]
    [SerializeField] InputAction _FadeNoir = null;
    [SerializeField] InputAction _ScnNebula = null;
    [SerializeField] InputAction _ScnSunshaft = null;
    [SerializeField] InputAction _ScnCam = null;
    [SerializeField] InputAction _ScnFX = null;
    [Header("NEXT")]
    [SerializeField] InputAction _ChangeFluid = null;
    [SerializeField] InputAction _ChangeFX = null;
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
    ///////////// FUNCTION
    float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
    {
        return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
    }
    void Start()
    {
        Noir = false;
    }
    void Update()
    {
        //float NewTargetPosition = PosX + 40;
        Vector3 NewTargetPosition = new Vector3(PosX+40,-4);
        MovableObject.transform.position = Vector3.SmoothDamp(MovableObject.transform.position, NewTargetPosition, ref velocity, SmoothT);
        //MovableObject.transform.position.x = f.SmoothDamp(MovableObject.transform.position.x, NewTargetPosition.x, ref velocity, SmoothT);

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

        _FXParam5.performed += FXParam5;
        _FXParam5.Enable();

        _FXParam6.performed += FXParam6;
        _FXParam6.Enable();

        _ChangeFluid.performed += ChangeFluid;
        _ChangeFluid.Enable();

        _ChangeFX.performed += ChangeFX;
        _ChangeFX.Enable();

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

        _ChangeFX.performed -= ChangeFX;
        _ChangeFX.Disable();

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

        _FXParam5.performed -= FXParam5;
        _FXParam5.Disable();

        _FXParam6.performed -= FXParam6;
        _FXParam6.Disable();

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
    }
    void BlurIntensity(InputAction.CallbackContext ctx)
    {
        BlurIntensityValue = ctx.ReadValue<float>();
        Mat_RenderFinal.sharedMaterial.SetFloat("BlurIntensity", BlurIntensityValue);
    }
    void RoughtIntensity(InputAction.CallbackContext ctx)
    {
        RoughtIntensityValue = ctx.ReadValue<float>();
        Mat_RenderFinal.sharedMaterial.SetFloat("RoughtIntensity", RoughtIntensityValue);
    }
    void IntensityControl(InputAction.CallbackContext ctx)
    {
        IntensityControlValue = ctx.ReadValue<float>();
        Mat_RenderFinal.sharedMaterial.SetFloat("Intensity", IntensityControlValue);
    }
    void ApparitionForme(InputAction.CallbackContext ctx)
    {
        AppFormeValue = ctx.ReadValue<float>();
        S_FinalRender.ApparitionForme = AppFormeValue;
        Mat_RenderFinal.sharedMaterial.SetFloat("_ApparitionForme", AppFormeValue);
    }
     void Taille(InputAction.CallbackContext ctx)
     {
         TailleValue = ctx.ReadValue<float>();
         S_FinalRender.Taille = TailleValue;
     }
    void Forme(InputAction.CallbackContext ctx)
    {
        FormeValue = ctx.ReadValue<float>();
        S_FinalRender.Forme = FormeValue;
    }
    void Disparition(InputAction.CallbackContext ctx)
    {
        DisparitionValue = ctx.ReadValue<float>();
        S_FinalRender.Disparition = DisparitionValue;
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
        float FxP1= ctx.ReadValue<float>() ;
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P1, FxP1);
    }
    void FXParam2(InputAction.CallbackContext ctx)
    {
        float FxP2 = ctx.ReadValue<float>();
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P2, FxP2);
    }
    void FXParam3(InputAction.CallbackContext ctx)
    {
        float FxP3 = ctx.ReadValue<float>();
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P3, FxP3);
    }
    void FXParam4(InputAction.CallbackContext ctx)
    {
        float FxP4 = ctx.ReadValue<float>();
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P4, FxP4);
    }
    void FXParam5(InputAction.CallbackContext ctx)
    {
        float FxP5 = ctx.ReadValue<float>();
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P5, FxP5);
    }
    void FXParam6(InputAction.CallbackContext ctx)
    {
        float FxP6 = ctx.ReadValue<float>();
        VisualEffect VFX = FX.GetComponent<VisualEffect>();
        VFX.SetFloat(Name_P6, FxP6);
    }
    void ChangeFluid(InputAction.CallbackContext ctx)
    {
        Manager.ChangeDisplace();
    }
    void ChangeFX(InputAction.CallbackContext ctx)
    {
        Manager.ChangeFX();
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
            Manager.AC.SetBool("FadeNoir",true);
            Noir = true;
        }
        else
        {
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
            SoundControl01 = false;
        }else{
            SoundControl01 = true;
        }
        /*
        if (Low)
        {
            Low = false;
            Medium = false;
            High = false;
        }else if (Medium)
        {
            Low = true;
            Medium = false;
            High = false;
        }else if (High)
        {
            Low = true;
            Medium = false;
            High = false;
        }*/
    }
    void ActiveSoundControl02(InputAction.CallbackContext ctx)
    {
        if (SoundControl02){
            SoundControl02 = false;
        }else{
            SoundControl02 = true;
        }
    }
    void ActiveSoundControl03(InputAction.CallbackContext ctx)
    {
        if (SoundControl03){
            SoundControl03 = false;
        } else{
            SoundControl03 = true;
        }
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




