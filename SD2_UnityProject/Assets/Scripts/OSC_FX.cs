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
            TLow += Low;
            TMid += Mid;
            THigh += High;
            SLow = Mathf.Lerp(SLow, Low, facMerge);
            SMid = Mathf.Lerp(SMid, Mid, facMerge);
            SHigh = Mathf.Lerp(SHigh, High, facMerge);

            if (S_Midi.SoundControl01 == true)
            {
                QuadRender.sharedMaterial.SetFloat("RoughtIntensity", S_Midi.RoughtIntensityValue*SHigh);
            }
            if (S_Midi.SoundControl02 == true)
            {
                QuadRender.sharedMaterial.SetFloat("Intensity", S_Midi.IntensityControlValue * SLow);
            }
            if (S_Midi.SoundControl03 == true)
            {
                QuadRender.sharedMaterial.SetFloat("_ApparitionForme", S_Midi.AppFormeValue * SMid);
            }
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
