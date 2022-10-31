using UnityEngine;
using System.Collections;
using Windows.Kinect;


public class SPT_KinectDepth : MonoBehaviour
{
    public GameObject DepthSourceManager;
    public Material Mat;
    private KinectSensor _Sensor;
    private CoordinateMapper _Mapper;
    private Vector3[] _Vertices;
    Texture2D texture;
    byte[] depthBitmapBuffer;
    /// Coordonnées Points 01

    /// CoordonnéesValue condition trigger event

    public float scale = 1.0f;
    public float scale2 = 0.0f;

    private const double _DepthScale = 0.1f;
    private const int _Speed = 50;
    private DepthSourceManager _DepthManager;

    private Vector2 g;
    private float vtot;
 
 
   
    void Start()
    {
        // KinectEvent.Touch1();
        

     
        g = new Vector2(0.0f, 0.0f);
        vtot = 0.0f;
    _Sensor = KinectSensor.GetDefault();
        if (_Sensor != null)
        {
            _Mapper = _Sensor.CoordinateMapper;
            var frameDesc = _Sensor.DepthFrameSource.FrameDescription;
           // H = frameDesc.Height / _DownsampleSizey;
           // W = frameDesc.Width / _DownsampleSizex;
           
            // Downsample to lower resolution
            //liste(frameDesc.Width / _DownsampleSizex, frameDesc.Height / _DownsampleSizey);
            depthBitmapBuffer = new byte[frameDesc.LengthInPixels * 3];
            texture = new Texture2D(frameDesc.Width, frameDesc.Height, TextureFormat.RGB24, false);
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }        
    }




    void Update()
    {

        if (_Sensor == null)
        {
            return;
        }
                         
            if (DepthSourceManager == null)
            {
                return;
            }            
            _DepthManager = DepthSourceManager.GetComponent<DepthSourceManager>();
            if (_DepthManager == null)
            {
                return;
            }
            
       
        
        updateTexture();


        //Mat.SetTexture("_MainTex" , texture);  
        gameObject.GetComponent<render02>().C = texture;


    }
    
 

  
    
    void updateTexture()
    {
        // get new depth data from DepthSourceManager.
        ushort[] rawdata = _DepthManager.GetData();

        // convert to byte data (
        for (int i = 0; i < rawdata.Length; i++)
        {

            byte value = (byte)(rawdata[i] * scale+scale2);
            // byte value2 = (byte)255;
            if (value < 0.05f) { value = 0; value = 255; }
            int colorindex = i * 3;
            depthBitmapBuffer[colorindex + 0] = value;
            depthBitmapBuffer[colorindex + 1] = value;
            depthBitmapBuffer[colorindex + 2] = value;
        }
        // make texture from byte array
        texture.LoadRawTextureData(depthBitmapBuffer);
        texture.Apply();
    }
    void OnApplicationQuit()
    {
        if (_Mapper != null)
        {
            _Mapper = null;
        }
        
        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }

            _Sensor = null;
        }
    }
}
