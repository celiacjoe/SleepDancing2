using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace OscSimpl.Examples
{
    public class OSC_Nebula : MonoBehaviour
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
        public string Name4;
        public string Name5;
        public string Name6;
        public string Name7;
        public string Name8;
        public string Name9;
        public string Name10;
        float v1; 
        public Vector3 fac;
        public Material mat;
      
        public NebulaRender scrpit1;

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
          
            mat.SetFloat("_SMid", SMid);
           
            scrpit1.Low = Low;
            scrpit1.TLow = TLow;
            scrpit1.SLow = SLow;
            scrpit1.Mid = Mid;
            scrpit1.TMid = TMid;
            scrpit1.SMid = SMid;
            scrpit1.High = High;
            scrpit1.THigh = THigh;
            scrpit1.SHigh = SHigh;
        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name1, Event1);
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);
            _oscIn.MapFloat(Name4, Event4);
            _oscIn.MapFloat(Name5, Event5);
            _oscIn.Map(Name6, Event6);
            _oscIn.MapFloat(Name7, Event7);
            _oscIn.MapFloat(Name8, Event8);
            _oscIn.MapFloat(Name9, Event9);
            _oscIn.MapFloat(Name10, Event10);

        }


        public void Event1(float value)
        {
            Low = value*v1;
        }
        public void Event2(float value)
        {
            Mid = value*v1;
        }
        public void Event3(float value)
        {
            High = value*v1;
        }
        public void Event4(float value)
        {
            scrpit1.transition1 = value ;
        }
        public void Event5(float value)
        {
            scrpit1.transition2 = value ;
        }

           void Event6(OscMessage message)
            {
                float fa;
                float fb;
                if (message.TryGet(0, out fa) && message.TryGet(1, out fb))
                {
                scrpit1.f1 = (fa - 0.5f)*-2;
                scrpit1.f2 = (fb - 0.5f)*2;
                    //Debug.Log("ok2");
                }
                OscPool.Recycle(message);
            }
        
        public void Event7(float value)
        {
            scrpit1.f3 = (value - 0.5f)*2;
        } 
        public void Event8(float value)
        {
            scrpit1.transition3 = value;

        }    
        public void Event9(float value)
        {
            v1 = Mathf.Pow(value,5)*20 + 0.001f;
        }
        public void Event10(float value)
        {
            scrpit1.transition4 = value;

        }

    }
}
