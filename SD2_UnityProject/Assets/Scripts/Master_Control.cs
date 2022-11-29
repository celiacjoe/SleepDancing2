using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Control : MonoBehaviour
{
    public Renderer Mat_RenderFinal;
    public float SharedBlurValue;
    public float SharedIntensityValue;
    public float SharedAppFormeValue;
    public float SharedTailleValue;
    public float SharedFormeValue;
    public float SharedDisparitionValue;

    public float SmoothBlurIntensity;
    public float SmoothIntensity;
    public float SmoothAppForme;
    public float SmoothRoughtIntensity;
    public float SmoothForme;
    public float SmoothTaille;
    public float SmoothDisparition;
    public float SmoothThick;
    public float SmoothColor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SmoothIntensity = Mathf.Lerp(SmoothIntensity, SharedIntensityValue, 0.05f);
        Mat_RenderFinal.sharedMaterial.SetFloat("Intensity", SmoothIntensity);
        //SmoothBlurIntensity = Mathf.Lerp(SmoothBlurIntensity, SharedBlurValue, 0.01f);
        //Mat_RenderFinal.sharedMaterial.SetFloat("BlurIntensity", SmoothBlurIntensity);
        //SmoothBlurIntensity = Mathf.Lerp(SmoothBlurIntensity, BlurIntensityValue, 0.01f);
        //Mat_RenderFinal.sharedMaterial.SetFloat("BlurIntensity", SmoothBlurIntensity);
    }
}
