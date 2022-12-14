using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace OscSimpl.Examples
{
    public class OSC_FX : MonoBehaviour
    {
        [SerializeField] OscIn _oscIn;    
        public string Name1;
        public float Low;
        public float TLow;
        public float SLow;
        public string Name2;
        public float Mid;
        public float TMid;
        public float SMid;
        public string Name3;
        public float High;
        public float THigh;
        public float SHigh;
        public float facMerge;
        public Vector3 fac;
        public InputMidiControl S_Midi;
        public Renderer QuadRender;
        public VideoDeform S_VideoDeform;
        public SceneManager S_Manager;
        public Master_Control S_MasterControl;
        public float RoughtSoundReactValue;
        public VisualEffect FX;
        public ShapeRender scriptshape;
        public string xy;
        public string nor;
        float noro;
        float fao;
        float fbo;
        public bool StandardActivated;

        float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
        {
            return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
        }

        float _incomingFloat;
        private int Nbr_portIn;

        void Start()
        {
            Nbr_portIn = _oscIn.port;
            if (!_oscIn) _oscIn = gameObject.AddComponent<OscIn>();
            _oscIn.Open(Nbr_portIn);
        }

        void Update()
        {
            fac.x = S_Midi.MultiplierSound01Value*10;
            fac.y = map(S_Midi.MultiplierSound02Value, 0, 1, 0, 50);
            fac.z = map(S_Midi.MultiplierSound03Value, 0, 1, 0, 50);

            S_MasterControl.AddSoundValue01 = Mathf.Pow(S_Midi.MultiplierSound02Value,5)* SMid;
            S_MasterControl.AddSoundValue02 = Mathf.Pow(S_Midi.MultiplierSound03Value, 5) * SHigh;
            TLow += Low;
            TMid += Mid;
            THigh += High;
            SLow = Mathf.Lerp(SLow, Low, facMerge);
            SMid = Mathf.Lerp(SMid, Mid, facMerge);
            SHigh = Mathf.Lerp(SHigh, High, facMerge);

            if (S_Midi.SoundControl01 == true)
            {         
                //QuadRender.sharedMaterial.SetFloat("RoughtIntensity", S_Midi.RoughtIntensityValue *Low * 5);
                QuadRender.sharedMaterial.SetFloat("RoughtIntensity", Mathf.Pow(S_Midi.RoughtIntensityValue,5)*Low*50);
                S_VideoDeform.RoughtIntensity=Mathf.Pow(S_Midi.RoughtIntensityValue,5)*Low*50;
            }
            if (S_Midi.SoundControl02 == true)
            {
                VisualEffect VFX = S_Manager.FX_List[S_Manager.Nbr_FX].GetComponent(typeof(VisualEffect)) as VisualEffect;
                VFX.SetFloat(S_Midi.Name_P2, Mathf.Pow(S_Midi.FxP2Value,5) * High* 100); // Opacity
                //VFX.SetFloat(S_Midi.Name_P2,Mathf.Pow(S_Midi.FxP2Value, 5) * Mid *10);
                VFX.SetFloat(S_Midi.Name_P3, Mathf.Pow(S_Midi.FxP3Value, 5) * Mid *80); // Thickness
               // VFX.SetFloat(S_Midi.Name_P4,Mathf.Pow(S_Midi.FxP4Value, 5) * SMid * map(S_Midi.MultiplierSoundValueFX,0,1,0,10));
               // QuadRender.sharedMaterial.SetFloat("Intensity", Mathf.Pow(S_Midi.IntensityValue, 5) * Low * 5);
            }/*
            if (S_Midi.SoundControl03 == true)
            {
                QuadRender.sharedMaterial.SetFloat("_ApparitionForme", Mathf.Pow(S_Midi.AppFormeValue, 5) * Low * 5);
            }*/
             //VisualEffect VFX = S_Manager.FX_List[S_Manager.Nbr_FX].GetComponent(typeof(VisualEffect)) as VisualEffect;
             //VisualEffect VFX = FX.GetComponent<VisualEffect>();
             //QuadRender.sharedMaterial.SetFloat("RoughtIntensity", Mathf.Pow(S_Midi.RoughtIntensityValue, 5) * Low * 5);

            //QuadRender.sharedMaterial.SetFloat("Intensity", S_Midi.IntensityControlValue + SMid * Mathf.Pow(S_Midi.MultiplierSound02Value,5) * 50);
            //QuadRender.sharedMaterial.SetFloat("_ApparitionForme", S_Midi.AppFormeValue + SHigh* Mathf.Pow(S_Midi.MultiplierSound03Value,5) * 50);
            if (StandardActivated == false)
            {
                QuadRender.sharedMaterial.SetFloat("NRMIntensity", noro);
                scriptshape.position1 =  fao;
                scriptshape.position2 =  fbo;
                
               
            }
            if (StandardActivated == true)
            {
                QuadRender.sharedMaterial.SetFloat("NRMIntensity", 0);
                scriptshape.position1 =  0;
                scriptshape.position2 =  0;
                
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Debug.Log("te");
                if (StandardActivated == false)
                {
                    StandardActivated = true;
                  
                }
                else
                {
                 
                    StandardActivated = false;
                }
            }
        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name1, Event1);
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);
            _oscIn.Map(xy, Event4);
            _oscIn.MapFloat(nor, Event5);
        }

        public void Event1(float value)
        {
            Low = value;
        }
        public void Event2(float value)
        {
            Mid = value;
        }
        public void Event3(float value)
        {
            High = value;
        }
        void Event4(OscMessage message)
        {
            float fa;
            float fb;
            if (message.TryGet(0, out fa) && message.TryGet(1, out fb))
            {
                fao = (fa - 0.5f) * -2;
                fbo = (fb - 0.5f) * 2;
                //Debug.Log("ok2");
            }
            OscPool.Recycle(message);
        }
        public void Event5(float value)
        {
          noro = value;
        }
    }
}
