using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class InputMidiControl : MonoBehaviour
{

    ///////////// VAR
    //public Renderer rend;
   // public renderLiquid1 SRend;
    public render SRend;
    public Camera Cam;
    //public GameObject GO;
    public SceneManager Manager;
    public Renderer RenderFinal;

    private Vector2 mousePosition;
    public VisualEffect FX;
    private float P1value;
    private float P2value;
    private float P3value;
    private float P4value;
    private float P5value;
    private float P6value;
    private float IntensityControlValue;
    private float ZoomValue;
    public float SmoothT = 0.3f;
    private Vector3 velocity;
    public GameObject MovableObject;
    private float PosX;
    private float PosY;

    [Header("DEFORMATION CONTROLL")]
    /////////////SET INPUT
    [SerializeField] InputAction _IntensityControl = null;
    [SerializeField] InputAction _P1_Deform = null;
    [SerializeField] InputAction _P5_Deform = null;
    [SerializeField] InputAction _P2_Deform = null;
    [SerializeField] InputAction _P3_Deform = null;
    [SerializeField] InputAction _P4_Deform = null;
    [SerializeField] InputAction _P6_Deform = null;
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

    [Header("PRESET ASSIGNMENT")]
    [SerializeField] InputAction _Set3DshapeSoft = null;
    [SerializeField] InputAction _Set3DshapeComplex = null;
    [SerializeField] InputAction __Set3DshapeNew = null;

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
        ////////////////// MOUSE POSITION
        mousePosition = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 NewTargetPosition = new Vector3(PosX+40, PosY);
        MovableObject.transform.position = Vector3.SmoothDamp(MovableObject.transform.position, NewTargetPosition, ref velocity, SmoothT);
    }

    void OnEnable()
    {
        _P1_Deform.performed += P1_Deform;
        _P1_Deform.Enable();

        _P2_Deform.performed += P2_Deform;
        _P2_Deform.Enable();

        _P3_Deform.performed += P3_Deform;
        _P3_Deform.Enable();

        _P4_Deform.performed += P4_Deform;
        _P4_Deform.Enable();

        _P5_Deform.performed += P5_Deform;
        _P5_Deform.Enable();

        _P6_Deform.performed += P6_Deform;
        _P6_Deform.Enable();

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

        _Set3DshapeSoft.performed += Set3DShapeSoft;
        _Set3DshapeSoft.Enable();

        _Set3DshapeComplex.performed += Set3DShapeComplex;
        _Set3DshapeComplex.Enable();
    }

    void OnDisable()
    {
        _P1_Deform.performed -= P1_Deform;
        _P1_Deform.Disable();

        _P2_Deform.performed -= P2_Deform;
        _P2_Deform.Disable();

        _P3_Deform.performed -= P3_Deform;
        _P3_Deform.Disable();

        _P4_Deform.performed -= P4_Deform;
        _P4_Deform.Disable();

        _P5_Deform.performed -= P5_Deform;
        _P5_Deform.Disable();

        _P6_Deform.performed -= P6_Deform;
        _P6_Deform.Disable();

        _LayerDisplace.performed -= DisplaceLayerChange;
        _LayerDisplace.Disable();

        _LayerBack.performed -= BackLayerChange;
        _LayerBack.Disable();

        _IntensityControl.performed -= IntensityControl;
        _IntensityControl.Disable();

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

        _Set3DshapeSoft.performed -= Set3DShapeSoft;
        _Set3DshapeSoft.Disable();

        _Set3DshapeComplex.performed -= Set3DShapeComplex;
        _Set3DshapeComplex.Disable();
    }

    void P1_Deform(InputAction.CallbackContext ctx)
     {
        P1value = ctx.ReadValue<float>();
        RenderFinal.sharedMaterial.SetFloat("BlurIntensity", P1value);
     }
     void P2_Deform(InputAction.CallbackContext ctx)
     {
         P2value = ctx.ReadValue<float>();
         SRend.Taille = P2value;
     }
    void P3_Deform(InputAction.CallbackContext ctx)
    {
        P3value = ctx.ReadValue<float>();
        SRend.Forme = P3value;
    }
    void P4_Deform(InputAction.CallbackContext ctx)
    {
        P4value = ctx.ReadValue<float>();
        SRend.Disparition = P4value;
    }
    void P5_Deform(InputAction.CallbackContext ctx)
    {
        P5value = ctx.ReadValue<float>();
        SRend.ApparitionForme = P5value;
        RenderFinal.sharedMaterial.SetFloat("_ApparitionForme", P5value);
    }

    void P6_Deform(InputAction.CallbackContext ctx)
    {
        P6value = ctx.ReadValue<float>();
        RenderFinal.sharedMaterial.SetFloat("Mask_Intensity", P6value);
    }
    void IntensityControl(InputAction.CallbackContext ctx)
     {
         IntensityControlValue = ctx.ReadValue<float>();
        RenderFinal.sharedMaterial.SetFloat("Intensity", IntensityControlValue);
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
        Manager.ChangeBack();
    }

    void ScnNebula(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Nebula";
        //RenderFinal.sharedMaterial.SetInt("_SunShaft_Nebula", 1);
        Manager.TransitionScene();
    }
    void ScnSunshaft(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Sunshaft";
        //RenderFinal.sharedMaterial.SetInt("_SunShaft_Nebula", 1);
        Manager.TransitionScene();
    }
    void ScnCam(InputAction.CallbackContext ctx)
    {
        Manager.Next = "Cam";
        //RenderFinal.sharedMaterial.SetInt("_SunShaft_Nebula", 1);
        Manager.TransitionScene();
    }
    void ScnFX(InputAction.CallbackContext ctx)
    {
        Manager.Next = "FX";
        //RenderFinal.sharedMaterial.SetInt("_SunShaft_Nebula", 1);
        Manager.TransitionScene();
    }
    void Set3DShapeSoft(InputAction.CallbackContext ctx)
    {
        Debug.Log("ok");
        Manager.SetupParam3DshapeSoft();
    }
    void Set3DShapeComplex(InputAction.CallbackContext ctx)
    {
        Manager.SetupParam3DshapeComplex();
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
    void ResetLevel(InputAction.CallbackContext ctx)
        {
            Application.LoadLevel(Application.loadedLevel);
            //Application.LoadLevel("SCN_Main");
        }

    }




