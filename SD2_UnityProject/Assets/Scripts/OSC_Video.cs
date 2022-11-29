using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace OscSimpl.Examples
{
    public class OSC_Video : MonoBehaviour
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
        public string Name4;
        public float High;
        public float THigh;
        public float SHigh;
        public float facMerge;
        //public Vector3 fac;
        //public Material mat;
        public string Name5;
        public string Name6;
        public string Name7;
        public string Name8;
        public string Name9;
        public string Name10;
        public string Name11;
        public string Name12;
        public string Name13;
        public string Name14;
        public string Name15;
        public string Name16;
        public VideoDeform scrpit1;
        public Master_Control script2 ;
       // public Material final;
        float v1;
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
           /*
            scrpit1.Low = Low;
            scrpit1.TLow = TLow;
            scrpit1.SLow = SLow;
            scrpit1.Mid = Mid;
            scrpit1.TMid = TMid;
            scrpit1.SMid = SMid;
            scrpit1.High = High;
            scrpit1.THigh = THigh;
            scrpit1.SHigh = SHigh;     */

        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name1, Event1);         
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);
            _oscIn.MapFloat(Name4, Event4);
            //_oscIn.Map(Name5, Event5);
            _oscIn.MapFloat(Name6, Event6);
            _oscIn.MapFloat(Name7, Event7);
            _oscIn.MapFloat(Name8, Event8);
            _oscIn.MapFloat(Name9, Event9);
            _oscIn.MapFloat(Name10, Event10);
            _oscIn.MapFloat(Name11, Event11);
            _oscIn.MapFloat(Name12, Event12);
            _oscIn.MapFloat(Name13, Event13);
            _oscIn.MapFloat(Name14, Event14);
            _oscIn.MapFloat(Name15, Event15);
            _oscIn.MapFloat(Name16, Event16);
        }


        public void Event1(float value)
        {
            Low = value*v1;
        }
        public void Event2(float value)
        {
            Mid = value* v1;
        }
        public void Event3(float value)
        {
            High = value* v1;
        }
        public void Event4(float value)
        {
            v1 = Mathf.Pow(value, 5) * 20+0.001f;
        }
        /*void Event5(OscMessage message)
        {
            float fa;
            float fb;
            if (message.TryGet(0, out fa) && message.TryGet(1, out fb))
            {
                scrpit1.m1 = fa  ;
                scrpit1.m2 = fb  ;
            }
            OscPool.Recycle(message);
        }   */
        public void Event6(float value)
        {
           scrpit1.Taille=value;
        }
        public void Event7(float value)
        {
            scrpit1.Forme = value;
        }
        public void Event8(float value)
        {
            scrpit1.Disparition = value;
        }
        public void Event9(float value)
        {
            scrpit1.dir = (value-0.5f)*2;
        }
        public void Event10(float value)
        {
            scrpit1.m1 = value;
        }
        public void Event11(float value)
        {
            scrpit1.m2 = value;
        }
        public void Event12(float value)
        {
            script2.SharedTailleValue = value;
        }
        public void Event13(float value)
        {
            script2.SharedFormeValue = value;
        }
        public void Event14(float value)
        {
            script2.SharedAppFormeValue = value;
        }
        public void Event15(float value)
        {
            script2.SharedDisparitionValue = value;
        }
        public void Event16(float value)
        {
            script2.SharedIntensityValue = value ;
        }

    }
}
