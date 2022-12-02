using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

namespace OscSimpl.Examples
{
    public class OSC_Video : MonoBehaviour
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
        public VideoDeform script1;
        public Master_Control script2 ;
        public Material final;
        public Material final2;
        public bool StandardActivated;
        float v1;
        float v1o;
        float Tailleo;
        float bvid;
        float Power;
        float diro;
        float Saturation;
        float _incomingFloat;
        float m1o;
        float m2o;
        float disparitiono;
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
            if (StandardActivated == false)
            {

                v1 = v1o;
                final.SetFloat("Power", Power);
                script1.Taille = Tailleo;
                final2.SetFloat("bvid", bvid);
                script1.dir = diro;
                final.SetFloat("Saturation", Saturation);
                script1.m1 = m1o;
                script1.m2 = m2o;
                script1.Disparition = disparitiono;
                script1.ms = TMid;
                S_UI.UI_OSCValue.GetComponentInChildren<Text>().text = "value" + v1o; /////// +++

            }
            if (StandardActivated == true)
            {
                v1 = 1;
                final.SetFloat("Power", 0.5f);
                script1.Taille = 0.2f;
                final2.SetFloat("bvid", 0.1f);
                script1.dir = -0.1f;
                final.SetFloat("Saturation", 0.5f);
                script1.m1 = 0.45f;
                script1.m2 = 0.55f;
                script1.Disparition = 0.05f;
                script1.ms = 0.1f* Time.time;

            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Debug.Log("te");
                if (StandardActivated == false)
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
            v1o = Mathf.Pow(value, 5) * 20+0.001f;
        }

        public void Event6(float value)
        {
            Power =value;
            Tailleo = value;
     
        }
        public void Event7(float value)
        {
            bvid = value;
            script2.SharedBlurValue = value;
        }
        public void Event8(float value)
        {
            disparitiono = value;
             
        }
        public void Event9(float value)
        {
            diro = (value-0.5f)*2;
            Saturation = value;
        }
        public void Event10(float value)
        {
            m1o = value;
        }
        public void Event11(float value)
        {
            m2o = value;
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
            script2.SharedAppLiquidValue = value;
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
