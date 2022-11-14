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
        public Vector3 fac;
        public Material mat;
        //public string Map_PosXY;
        //public Vector2 V2;
        //public VisualEffect FX;
        public GameObject GObj;
        public string script;

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
            TMid += SMid;
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
            GObj.GetComponent<NebulaRender>().Low = Low;
            GObj.GetComponent<NebulaRender>().TLow = TLow;
            GObj.GetComponent<NebulaRender>().SLow = SLow;
            GObj.GetComponent<NebulaRender>().Mid = Mid;
            GObj.GetComponent<NebulaRender>().TMid = TMid;
            GObj.GetComponent<NebulaRender>().SMid = SMid;
            GObj.GetComponent<NebulaRender>().High = High;
            GObj.GetComponent<NebulaRender>().THigh = THigh;
            GObj.GetComponent<NebulaRender>().SHigh = SHigh;

        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name1, Event1);           // Float 
            _oscIn.MapFloat(Name2, Event2);
            _oscIn.MapFloat(Name3, Event3);

            //  _oscIn.Map(Map_PosXY, EventPosXY);          // MultiFloat

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
