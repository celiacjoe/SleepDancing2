using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class InputMidiControl : MonoBehaviour
{
    /////////////SET INPUT
    [SerializeField] InputAction _P1 = null;
    [SerializeField] InputAction _P2 = null;
    [SerializeField] InputAction _LayerChange = null;
    [SerializeField] InputAction _IntensityControl = null;
    [SerializeField] InputAction _Zoom = null;
    [SerializeField] InputAction _ResetLevel = null;
    [SerializeField] InputAction _PositionX = null;
    [SerializeField] InputAction _PositionY = null;

    ///////////// FUNCTION
    float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
    {
        return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
    }

    ///////////// VAR
    public Renderer rend;
    public render SRend;
    public Camera Cam;
    public Camera CamMask;
    public GameObject GO;
    public Vector2 mousePosition;

    private int L;
    private float P1value;
    private float P2value;
    private float IntensityControlValue;
    private float ZoomValue;
    public float SmoothT = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocityCam = Vector3.zero;
    public GameObject MovableObject;
  //  public Vector3 PosXY;
    public float PosX;
    public float PosY;

    void Start()
    {
        L = 1;
    }
    void Update()
    {
        ////////////////// MOUSE POSITION
        mousePosition = Cam.ScreenToWorldPoint(Input.mousePosition);
        GO.transform.position = new Vector3(mousePosition.x - 20, mousePosition.y, 0);
        ////////////////// SMOOTH POSITION CHANGE
      //  Vector3 NewTargetPosX = PositionX;
      //  MovableObject.transform.position = Vector3.SmoothDamp(MovableObject.transform.position, NewTargetPosX, ref velocity, SmoothT);

        Vector3 NewTargetPosition = new Vector3(PosX, PosY);
        MovableObject.transform.position = Vector3.SmoothDamp(MovableObject.transform.position, NewTargetPosition, ref velocity, SmoothT);
    }

    void OnEnable()
    {
        _P1.performed += P1;
        _P1.Enable();

        _P2.performed += P2;
        _P2.Enable();

        _LayerChange.performed += LayerChange;
        _LayerChange.Enable();

        _IntensityControl.performed += IntensityControl;
        _IntensityControl.Enable();

        _Zoom.performed += Zoom;
        _Zoom.Enable();

        _PositionX.performed += PositionX;
        _PositionX.Enable();

        _PositionY.performed += PositionY;
        _PositionY.Enable();

        /*

                        ///// DEBUG STUFF
                        _DebugClean.performed += DebugClean;
                        _DebugClean.Enable();
                        _DebugFunction.performed += DebugFunction;
                        _DebugFunction.Enable();

                _ResetLevel.performed += ResetLevel;
                _ResetLevel.Enable();      */
    }

    void OnDisable()
    {
        _P1.performed -= P1;
        _P1.Disable();

        _P2.performed -= P2;
        _P2.Disable();

        _LayerChange.performed -= LayerChange;
        _LayerChange.Disable();

        _IntensityControl.performed -= IntensityControl;
        _IntensityControl.Disable();

        _Zoom.performed -= Zoom;
        _Zoom.Disable();

        _PositionX.performed -= PositionX;
        _PositionX.Disable();

        _PositionY.performed -= PositionY;
        _PositionY.Disable();

        /*
          ////// DEBUG STUFF
          _DebugClean.performed -= DebugClean;
          _DebugClean.Disable();
          _DebugFunction.performed -= DebugFunction;
          _DebugFunction.Disable(); 
           _ResetLevel.performed -= ResetLevel;
          _ResetLevel.Disable();
          */
    }
    void LayerChange(InputAction.CallbackContext ctx)
    {
        if (L == 1)
        {
            L++;
        } else if (L == 2) {
            L++;
        } else if (L == 3) {
            L++;
        } else if (L == 4) {
            L++;
        } else if (L > 4)
        {
            L = 1;
        }
    }

     void P1(InputAction.CallbackContext ctx)
     {
         P1value = ctx.ReadValue<float>();
         SRend.Taille = P1value;
     }
     void P2(InputAction.CallbackContext ctx)
     {
         P2value = ctx.ReadValue<float>();
         SRend.Forme= P2value;
     }
     void IntensityControl(InputAction.CallbackContext ctx)
     {
         IntensityControlValue = ctx.ReadValue<float>();
         rend.sharedMaterial.SetFloat("Intensity", IntensityControlValue);
     }

     void Zoom(InputAction.CallbackContext ctx)
     {
         ZoomValue = ctx.ReadValue<float>();
         Debug.Log(ZoomValue);
         Cam.orthographicSize = map(ZoomValue,0, 1, 0.25f, 4.5f);
     }

    void PositionX(InputAction.CallbackContext ctx)
    {
        PosX = ctx.ReadValue<float>();
        Debug.Log(PosX);
        Cam.orthographicSize = map(ZoomValue, 0, 1, -10f, 10f);
    }

    void PositionY(InputAction.CallbackContext ctx)
    {
        PosY = ctx.ReadValue<float>();
        Debug.Log(PosY);
        Cam.orthographicSize = map(PosY, 0, 1, -5f, 5f);
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




