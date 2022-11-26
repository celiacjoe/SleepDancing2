using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class InputMidiControl : MonoBehaviour
{

    ///////////// VAR
    public render SRend;
    public Camera Cam;
    //public GameObject GO;
    public SceneManager Manager;
    public Renderer RenderFinal;
    public VisualEffect FX;
    public bool AppFormSound = false;
    public bool TXTGrain = false;
    public bool SoundIntensity = false;
    private float BlurIntensityValue;
    private float RoughtIntensityValue;
    private float IntensityControlValue;
    private float AppFormeValue;
    private float TailleValue;
    private float TailleValue2;
    private float FormeValue;
    private float DisparitionValue;
    private float ZoomValue;
    public float SmoothT = 0.45f;
    private Vector3 velocity;
    public GameObject MovableObject;
    private float PosX;
    private float PosY;

    [Header("DEFORMATION CONTROLL")]
    /////////////SET INPUT
    [SerializeField] InputAction _BlurIntensity = null;
    [SerializeField] InputAction _RoughtIntensity = null;
    [SerializeField] InputAction _IntensityControl = null;
    [SerializeField] InputAction _ApparitionForme = null;
    [SerializeField] InputAction _Taille = null;
    [SerializeField] InputAction _Taille2 = null;
    [SerializeField] InputAction _Forme = null;
    [SerializeField] InputAction _Disparition = null;
    [SerializeField] InputAction _Zoom = null;
    // [SerializeField] InputAction _ResetLevel = null;
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
    [SerializeField] InputAction _LayerDisplace = null;
    [SerializeField] InputAction _LayerBack = null;
    [SerializeField] InputAction _ScnNebula = null;
    [SerializeField] InputAction _ScnSunshaft = null;
    [SerializeField] InputAction _ScnCam = null;
    [SerializeField] InputAction _ScnFX = null;
    [SerializeField] InputAction _ScnGrain = null;

    [Header("PRESET ASSIGNMENT")]
    [SerializeField] InputAction _Set3DshapeSoft = null;
    [SerializeField] InputAction _Set3DshapeComplex = null;
    [SerializeField] InputAction _Set3DShapePetit = null;

    [Header("SOUND CONTROL")]
    [SerializeField] InputAction _ActiveSoundControlAppForm = null;
    [SerializeField] InputAction _ActiveSoundControlTXTGrain = null;
    [SerializeField] InputAction _ActiveSoundControlIntensity = null;
    ///////////// FUNCTION
    float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
    {
        return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
    }
    void Start()
    {
     
    }
    void Update()
    {      
        Vector3 NewTargetPosition = new Vector3(PosX+40, PosY);
        MovableObject.transform.position = Vector3.SmoothDamp(MovableObject.transform.position, NewTargetPosition, ref velocity, SmoothT);
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

        _LayerDisplace.performed += DisplaceLayerChange;
        _LayerDisplace.Enable();

        _LayerBack.performed += BackLayerChange;
        _LayerBack.Enable();

        _ScnNebula.performed += ScnNebula;
        _ScnNebula.Enable();

        _ScnSunshaft.performed += ScnSunshaft;
        _ScnSunshaft.Enable();

        _ScnCam.performed += ScnCam;
        _ScnCam.Enable();

        _ScnFX.performed += ScnFX;
        _ScnFX.Enable();

        _ScnGrain.performed += ScnGrain;
        _ScnGrain.Enable();

        _Set3DshapeSoft.performed += Set3DShapeSoft;
        _Set3DshapeSoft.Enable();

        _Set3DshapeComplex.performed += Set3DShapeComplex;
        _Set3DshapeComplex.Enable();

        _Set3DShapePetit.performed += Set3DShapePetit;
        _Set3DShapePetit.Enable();

        _ActiveSoundControlAppForm.performed += SetSoundControlAppForm;
        _ActiveSoundControlAppForm.Enable();

        _ActiveSoundControlTXTGrain.performed += SetSoundControlTXTGrain;
        _ActiveSoundControlTXTGrain.Enable();

        _ActiveSoundControlIntensity.performed += SetSoundControlIntensity;
        _ActiveSoundControlIntensity.Enable();
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

        _LayerDisplace.performed -= DisplaceLayerChange;
        _LayerDisplace.Disable();

        _LayerBack.performed -= BackLayerChange;
        _LayerBack.Disable();

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

        _ScnNebula.performed -= ScnNebula;
        _ScnNebula.Disable();

        _ScnSunshaft.performed -= ScnSunshaft;
        _ScnSunshaft.Disable();

        _ScnCam.performed -= ScnCam;
        _ScnCam.Disable();

        _ScnFX.performed -= ScnFX;
        _ScnFX.Disable();

        _ScnGrain.performed -= ScnGrain;
        _ScnGrain.Disable();

        _Set3DshapeSoft.performed -= Set3DShapeSoft;
        _Set3DshapeSoft.Disable();

        _Set3DshapeComplex.performed -= Set3DShapeComplex;
        _Set3DshapeComplex.Disable();

        _Set3DShapePetit.performed -= Set3DShapePetit;
        _Set3DShapePetit.Disable();

        _ActiveSoundControlAppForm.performed -= SetSoundControlAppForm;
        _ActiveSoundControlAppForm.Disable();

        _ActiveSoundControlTXTGrain.performed -= SetSoundControlTXTGrain;
        _ActiveSoundControlTXTGrain.Disable();

        _ActiveSoundControlIntensity.performed -= SetSoundControlIntensity;
        _ActiveSoundControlIntensity.Disable();
    }
    void BlurIntensity(InputAction.CallbackContext ctx)
    {
        BlurIntensityValue = ctx.ReadValue<float>();
        RenderFinal.sharedMaterial.SetFloat("BlurIntensity", BlurIntensityValue);
    }
    void RoughtIntensity(InputAction.CallbackContext ctx)
    {
        RoughtIntensityValue = ctx.ReadValue<float>();
        RenderFinal.sharedMaterial.SetFloat("RoughtIntensity", RoughtIntensityValue);
    }
    void IntensityControl(InputAction.CallbackContext ctx)
    {
        IntensityControlValue = ctx.ReadValue<float>();
        RenderFinal.sharedMaterial.SetFloat("Intensity", IntensityControlValue);
    }
    void ApparitionForme(InputAction.CallbackContext ctx)
    {
        AppFormeValue = ctx.ReadValue<float>();
        SRend.ApparitionForme = AppFormeValue;
        RenderFinal.sharedMaterial.SetFloat("_ApparitionForme", AppFormeValue);
    }
     void Taille(InputAction.CallbackContext ctx)
     {
         TailleValue = ctx.ReadValue<float>();
         SRend.Taille = TailleValue;
     }
    void Forme(InputAction.CallbackContext ctx)
    {
        FormeValue = ctx.ReadValue<float>();
        SRend.Forme = FormeValue;
    }
    void Disparition(InputAction.CallbackContext ctx)
    {
        DisparitionValue = ctx.ReadValue<float>();
        SRend.Disparition = DisparitionValue;
    }
    void Taille2(InputAction.CallbackContext ctx)
    {
        TailleValue2 = ctx.ReadValue<float>();
        SRend.Taille2 = TailleValue2;
    }
    void Zoom(InputAction.CallbackContext ctx)
     {
         ZoomValue = ctx.ReadValue<float>();
         Cam.orthographicSize = map(ZoomValue,0, 1, 0.25f, 4.5f);
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
    void DisplaceLayerChange(InputAction.CallbackContext ctx)
    {
        Manager.ChangeDisplace();
    }
    void BackLayerChange(InputAction.CallbackContext ctx)
    {
        Debug.Log(Manager.Nbr_SceneB);
        Manager.ChangeBack();
    }
    void ScnGrain(InputAction.CallbackContext ctx)
    {
        Manager.ChangeGrainTexture();
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
        Manager.SetupParam3DshapeSoft();
    }
    void Set3DShapeComplex(InputAction.CallbackContext ctx)
    {
        Manager.SetupParam3DshapeComplex();
    }

    void Set3DShapePetit(InputAction.CallbackContext ctx)
    {
        Manager.SetupParam3DshapePetit();
    }

    void SetSoundControlAppForm(InputAction.CallbackContext ctx)
    {
        if (AppFormSound)
        {
            AppFormSound = false;
        }
        else
        {
            AppFormSound = true;
        }
    }
    void SetSoundControlTXTGrain(InputAction.CallbackContext ctx)
    {
        if (TXTGrain)
        {
            TXTGrain = false;
        }
        else
        {
            TXTGrain = true;
        }
    }
    void SetSoundControlIntensity(InputAction.CallbackContext ctx)
    {
        if (SoundIntensity)
        {
            SoundIntensity = false;
        }
        else
        {
            SoundIntensity = true;
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




