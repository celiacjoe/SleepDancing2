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
        public float RoughtSoundReactValue;

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
            fac.x = S_Midi.MultiplierSound01Value*5;
            fac.y = map(S_Midi.MultiplierSound02Value, 0, 1, 0, 40);
            fac.z = map(S_Midi.MultiplierSound03Value, 0, 1, 0, 40);

            TLow += Low;
            TMid += Mid;
            THigh += High;
            SLow = Mathf.Lerp(SLow, Low, facMerge);
            SMid = Mathf.Lerp(SMid, Mid, facMerge);
            SHigh = Mathf.Lerp(SHigh, High, facMerge);

            if (S_Midi.SoundControl01 == true)
            {         
                //QuadRender.sharedMaterial.SetFloat("RoughtIntensity", S_Midi.RoughtIntensityValue *Low * 5);
                QuadRender.sharedMaterial.SetFloat("RoughtIntensity", Mathf.Pow(S_Midi.RoughtIntensityValue,5)*Low*5);
                S_VideoDeform.RoughtIntensity=Mathf.Pow(S_Midi.RoughtIntensityValue,5)*Low*5;
            }

            /*if (S_Midi.SoundControl02 == true)
            {
                QuadRender.sharedMaterial.SetFloat("Intensity", S_Midi.IntensityControlValue + SMid*S_Midi.MultiplierSound02Value*20);
            }
            if (S_Midi.SoundControl03 == true)
            {
                QuadRender.sharedMaterial.SetFloat("_ApparitionForme", S_Midi.AppFormeValue * SHigh);
            }*/
            QuadRender.sharedMaterial.SetFloat("Intensity", S_Midi.IntensityControlValue + SMid * Mathf.Pow(S_Midi.MultiplierSound02Value,5) * 20);
            QuadRender.sharedMaterial.SetFloat("_ApparitionForme", S_Midi.AppFormeValue + SHigh* Mathf.Pow(S_Midi.MultiplierSound03Value,5) * 20);
        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name1, Event1);
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);   
        }

        public void Event1(float value)
        {
            Low = value*fac.x;
        }
        public void Event2(float value)
        {
            Mid = value*fac.y;
        }
        public void Event3(float value)
        {
            High = value*fac.z;
        }
    }
}
