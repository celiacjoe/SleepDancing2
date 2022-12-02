using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

namespace OscSimpl.Examples
{
    public class OSC_Sound : MonoBehaviour
    {
        [SerializeField] OscIn _oscIn;
        public UI_Manager S_UI;
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
        public Vector3 fac;
        public Material mat;
        public string Name5;
        public string Name6;
        
        public SoundRender scrpit1;
        public bool StandardActivated;
        float v1;
        float v1o;
        float fao;
        float fbo;

        float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
        {
            return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
        }

        float _incomingFloat;
        private int Nbr_portIn;

        void Start()
        {
            StandardActivated = false;
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

            scrpit1.Low = Low;
            scrpit1.TLow = TLow;
            scrpit1.SLow = SLow;
            scrpit1.Mid = Mid;
            scrpit1.TMid = TMid;
            scrpit1.SMid = SMid;
            scrpit1.High = High;
            scrpit1.THigh = THigh;
            scrpit1.SHigh = SHigh;

             if(StandardActivated == false)
            {
                S_UI.UI_OSCValue.GetComponentInChildren<Text>().text = "value" + v1o; /////// +++
                v1 = v1o;
                scrpit1.f1 = fao;
                scrpit1.f2 = fbo;
            }
            if(StandardActivated == true)
            {
                v1 = 1;
                scrpit1.f1 = 0;
                scrpit1.f2 = 0;

            }
            if (Input.GetKeyDown(KeyCode.K))
            {
               // Debug.Log("te");
                if (StandardActivated==false)
                {

                    S_UI.UI_OSC.SetActive(true);/////// +++
                    StandardActivated = true;
                }

                else
                {
                    S_UI.UI_OSC.SetActive(false);/////// +++
                    StandardActivated = false;
                }
                 
            }

        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name1, Event1);         
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);
            _oscIn.MapFloat(Name4, Event4);
            _oscIn.Map(Name5, Event5);
            _oscIn.MapFloat(Name6, Event6);

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
                v1o = Mathf.Pow(value, 5) * 20 + 0.001f;           
        }
        void Event5(OscMessage message)
        {
            float fa;
            float fb;
            if (message.TryGet(0, out fa) && message.TryGet(1, out fb))
                {
                    fao = (fa - 0.5f) * 2;
                    fbo = (fb - 0.5f) * 2;
                  
                }
                OscPool.Recycle(message);
            
        }
        public void Event6(float value)
        {
        
                scrpit1.f3 = 0;
           
         
            scrpit1.f3= value;
        }
    }
}
