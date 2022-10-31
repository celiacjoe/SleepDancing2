using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class InputMidiControl : MonoBehaviour
{

    [SerializeField] InputAction _P1 = null;
    [SerializeField] InputAction _P2 = null;

    [SerializeField] InputAction _LayerChange = null;
    [SerializeField] InputAction _IntensityControl = null;

    [SerializeField] InputAction _Zoom = null;
    //[SerializeField] InputAction _NouvelUnivers = null;

    [SerializeField] InputAction _ResetLevel = null;


    float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
     {
         return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
     }

    public Renderer rend;
    public render SRend;
    public Camera Cam;

    private int L;
    private float P1value;
    private float P2value;
    private float IntensityControlValue;
    private float ZoomValue;


    void Start()
    {
       L = 1;
    }
    void Update()
    {

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
        /*
                _AssignCurrentUnivers.performed += AssignCurrentUnivers;
                _AssignCurrentUnivers.Enable();

                _NouvelUnivers.performed += NouvelUnivers;
                _NouvelUnivers.Enable();

                _ChangeSpeed.performed += ChangeSpeed;
                _ChangeSpeed.Enable();

                _NextSequence.performed += NextSequence;
                _NextSequence.Enable();

                _Addition.performed += Addition;
                _Addition.Enable();

                _Fade.performed += Fade;
                _Fade.Enable();

                _AssignBlink.performed += Blink;
                _AssignBlink.Enable();

                _SwitchPP.performed += SwitchPP;
                _SwitchPP.Enable();

                _SwitchCam.performed += SwitchCam;
                _SwitchCam.Enable();

                _AnimCam.performed += AnimCam;
                _AnimCam.Enable();

                _Typo.performed += Typo;
                _Typo.Enable();

                _UI.performed += UI;
                _UI.Enable();

                _ChangeSceneTransition.performed += ChangeSceneTransition;
                _ChangeSceneTransition.Enable();

                _ChangeSceneMain.performed += ChangeSceneMain;
                _ChangeSceneMain.Enable();

                ///// DEBUG STUFF
                _DebugClean.performed += DebugClean;
                _DebugClean.Enable();
                _DebugFunction.performed += DebugFunction;
                _DebugFunction.Enable();
          */
        _ResetLevel.performed += ResetLevel;
        _ResetLevel.Enable();

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

        /*
                _Subdivision2.performed -= Subdivision2;
                _Subdivision2.Disable();

                _AssignCurrentUnivers.performed += AssignCurrentUnivers;
                _AssignCurrentUnivers.Disable();

                _NouvelUnivers.performed -= NouvelUnivers;
                _NouvelUnivers.Disable();

                _NextSequence.performed -= NextSequence;
                _NextSequence.Disable();

                _ChangeSpeed.performed -= ChangeSpeed;
                _ChangeSpeed.Disable();

                _Addition.performed -= Addition;
                _Addition.Disable();

                _Fade.performed += Fade;
                _Fade.Disable();

                _AssignBlink.performed -= Blink;
                _AssignBlink.Disable();

                _SwitchPP.performed += SwitchPP;
                _SwitchPP.Disable();

                _SwitchCam.performed += SwitchCam;
                _SwitchCam.Disable();

                _AnimCam.performed -= AnimCam;
                _AnimCam.Disable();

                _Typo.performed -= Typo;
                _Typo.Disable();

                _UI.performed -= UI;
                _UI.Disable();

                _ChangeSceneTransition.performed -= ChangeSceneTransition;
                _ChangeSceneTransition.Disable();

                _ChangeSceneMain.performed -= ChangeSceneMain;
                _ChangeSceneMain.Disable();

                ////// DEBUG STUFF
                _DebugClean.performed -= DebugClean;
                _DebugClean.Disable();
                _DebugFunction.performed -= DebugFunction;
                _DebugFunction.Disable();*/
        _ResetLevel.performed -= ResetLevel;
                _ResetLevel.Disable();
        
    }
    void LayerChange(InputAction.CallbackContext ctx)
    {
        if (L ==1)
        {
            L++;
        } else if (L==2)
        {
            L++;
        }else if (L == 3)
        {
            L++;
        }else if (L == 4)
        {
            L++;
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
        Cam.orthographicSize = map(ZoomValue,0, 1, 0.5f, 4.5f);
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

      void Subdivision2(InputAction.CallbackContext ctx)
      {
          if (!Sequence.Subdivision2){
              UI_Frag.SetActive(true);
              Sequence.Subdivision2 = true;
          } else{
              UI_Frag.SetActive(false);
              Sequence.Subdivision2 = false;
          }
      }

      void AssignCurrentUnivers(InputAction.CallbackContext ctx)
      {
          if (!Sequence.AssignCurrentUnivers){
              UI_AssignScene.SetActive(true);
              Sequence.AssignCurrentUnivers = true;
          }else{
              UI_AssignScene.SetActive(false);
              Sequence.AssignCurrentUnivers = false;
          }
      }

      void NouvelUnivers(InputAction.CallbackContext ctx)
      {
          if (!Sequence.NouvelUnivers)
          {
              UI_NewUnivers.SetActive(true);
              Sequence.NouvelUnivers = true;
          }else{
              UI_NewUnivers.SetActive(false);
              Sequence.NouvelUnivers = false;
          }
      }

      void Addition(InputAction.CallbackContext ctx)
      {
          if (!Sequence.Add)
          {
              UI_Add.SetActive(true);
              Sequence.Add = true;
          }else{
              UI_Add.SetActive(false);
              Sequence.Add = false;
          }
      }

      void Fade(InputAction.CallbackContext ctx)
      {
          if (!Sequence.Fade){
              UI_Fade.SetActive(true);
              Sequence.Fade = true;
          }else{
              UI_Fade.SetActive(false);
              Sequence.Fade = false;
          }
      }

      void NextSequence(InputAction.CallbackContext ctx)
      {
          if (!Sequence.Next){
              UI_ChangePhase.SetActive(true);
              Sequence.Next = true;
          }else{
              UI_ChangePhase.SetActive(false);
              Sequence.Next = false;
          }
      }

      void ChangeSpeed(InputAction.CallbackContext ctx)
      {
          if(!Sequence.Speed){
              UI_Speed.SetActive(true);
              Sequence.Speed = true;
          } else{
              UI_Speed.SetActive(false);
              Sequence.Speed = false;
          }
      }

      void DebugFunction(InputAction.CallbackContext ctx)
      {
          Compo.Landscape();
      }

      void DebugClean(InputAction.CallbackContext ctx)
      {
          Compo.Clean();
      }

      void Blink(InputAction.CallbackContext ctx)
      {
          Compo.AssignBlink = true;
      }

      void SwitchPP(InputAction.CallbackContext ctx)
      {
        /*  if (!Sequence.PP){
              Sequence.PP = true;
          }else{
              Sequence.PP = false;
          }
      }

      void SwitchCam(InputAction.CallbackContext ctx)
      {      
          Cam.SwitchCamOrtho();         
      }

      void AnimCam(InputAction.CallbackContext ctx)
      {
          Compo.AnimCam();
      }

      void Typo(InputAction.CallbackContext ctx)
      {
          if (!Sequence.Typo){
              UI_Typo.SetActive(true);
              Sequence.Typo = true;
          }else{
              UI_Typo.SetActive(false);
              Sequence.Typo = false;
          }
      }

      void UI(InputAction.CallbackContext ctx)
      {
          if (!Sequence.UI){
              UI_GPS.SetActive(true);
              Sequence.UI = true;
          } else {
              UI_GPS.SetActive(false);
              Sequence.UI = false;
          }
      }

      public void CleanUI()
      {
          UI_ScreenA.SetActive(false);
          UI_ScreenB.SetActive(false);
          UI_Add.SetActive(false);
          UI_Subdiv.SetActive(false);
          UI_Frag.SetActive(false);
          UI_AssignScene.SetActive(false);
          UI_NewUnivers.SetActive(false);
          UI_ChangePhase.SetActive(false);
          UI_Typo.SetActive(false);
          UI_Cam.SetActive(false);
          UI_Fade.SetActive(false);
          UI_GPS.SetActive(false);
          UI_Speed.SetActive(false);
      }

      void ResetLevel(InputAction.CallbackContext ctx)
      {
          Application.LoadLevel(Application.loadedLevel);
      }

      void ChangeSceneTransition(InputAction.CallbackContext ctx)
      {
          Application.LoadLevel("Scn_Transition");
      }


    */

    void ResetLevel(InputAction.CallbackContext ctx)
    {
        Application.LoadLevel(Application.loadedLevel);
        //Application.LoadLevel("SCN_Main");
    }

}

