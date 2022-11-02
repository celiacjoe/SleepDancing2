using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelColor : MonoBehaviour
{

    public Texture2D Map;
    public Vector3 PosValue;
    public int ValueX;
    public int ValueY;
    public Color C;
    public GameObject GO;

    public float Factor;

    void Start()
    {

    }
    void Update()
    {
        // int x = Mathf.FloorToInt(transform.position.x / size.x * heightmap.width);
        // int z = Mathf.FloorToInt(transform.position.z / size.z * heightmap.height);
        //  Vector3 pos = transform.position;
        //  pos.y = heightmap.GetPixel(x, z).grayscale * size.y;
        // transform.position = pos;

       // PosValue = GO.transform.position * 10;

        PosValue.x = Map.GetPixel(ValueX, ValueY).r ;
        PosValue.y = Map.GetPixel(ValueX, ValueY).g ;
        PosValue.z = Map.GetPixel(ValueX, ValueY).b ;
        C = new Color(PosValue.x, PosValue.y, PosValue.z);
        GO.transform.position = PosValue * Factor;
        //PosValue.y = Map.GetPixel(ValueX+40, ValueY+40).b;
    }
}
