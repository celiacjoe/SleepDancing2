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
        /* public string Name9;
         public string Name10;
         public string Name11; */
        float v1; /*
        float v2;
        float v3;  */
        public Vector3 fac;
        public Material mat;
       /* public float f1;
        public float f2;
        public float f3; */
       /* public float f4;
        public float f5;   */
        //public string Map_PosXY;
        //public Vector2 V2;
        //public VisualEffect FX;
       // public GameObject GObj;
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
            /*mat.SetFloat("_Low", Low);
            mat.SetFloat("_TLow", TLow);
            mat.SetFloat("_SLow", SLow);
            mat.SetFloat("_Mid", Mid);
            mat.SetFloat("_TMid", TMid);
            mat.SetFloat("_SMid", SMid);
            mat.SetFloat("_High", High);
            mat.SetFloat("_THigh", THigh);
            mat.SetFloat("_SHigh", SHigh);    */
            mat.SetFloat("_SMid", SMid);
            /* GObj.GetComponent<NebulaRender>().Low = Low;
             GObj.GetComponent<NebulaRender>().TLow = TLow;
             GObj.GetComponent<NebulaRender>().SLow = SLow;
             GObj.GetComponent<NebulaRender>().Mid = Mid;
             GObj.GetComponent<NebulaRender>().TMid = TMid;
             GObj.GetComponent<NebulaRender>().SMid = SMid;
             GObj.GetComponent<NebulaRender>().High = High;
             GObj.GetComponent<NebulaRender>().THigh = THigh;
             GObj.GetComponent<NebulaRender>().SHigh = SHigh;  */
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
            _oscIn.MapFloat(Name1, Event1);           // Float 
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);
            _oscIn.MapFloat(Name4, Event4);
            _oscIn.MapFloat(Name5, Event5);
            _oscIn.Map(Name6, Event6);
            _oscIn.MapFloat(Name7, Event7);
            _oscIn.MapFloat(Name8, Event8);
            _oscIn.MapFloat(Name9, Event9);
            /*_oscIn.MapFloat(Name10, Event10);
            _oscIn.MapFloat(Name11, Event11);  */

            //  _oscIn.Map(Map_PosXY, EventPosXY);          // MultiFloat

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
            v1 = value*8;
        }      /*
        public void Event10(float value)
        {
            v2 = value;
        }
        public void Event11(float value)
        {
            v3 = value;
        }     */
        /* void EventPosXY(OscMessage message)
         {
             float f1;
             float f2;
             if (message.TryGet(0, out f1) && message.TryGet(1, out f2))
             {
                 V2.x = map(f1, 0, 1, -7, 4);
                 V2.y = map(f2, 0, 1, -7, 4);
                 Debug.Log("ok2");
             }
             OscPool.Recycle(message);
         } */



    }
}
