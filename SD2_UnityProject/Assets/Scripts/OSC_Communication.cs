using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace OscSimpl.Examples
{
    public class OSC_Communication : MonoBehaviour
    {
        [SerializeField] OscIn _oscIn;
        public float f;
        public string Name;
        public string Map_PosXY;
        public Vector2 V2;
        //public VisualEffect FX;

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

        }

        void OnEnable()
        {
            _oscIn.MapFloat(Name, EventTest);           // Float 
            
            _oscIn.Map(Map_PosXY, EventPosXY);          // MultiFloat

        }


        public void EventTest(float value)
        {
            f = value;
        }

        void EventPosXY(OscMessage message)
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
        }



    }
}
