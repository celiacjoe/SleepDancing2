using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Control : MonoBehaviour
{
    public float AddSoundValue01;
    public float AddSoundValue02;
    [Header("IN")]
    public float SharedBlurValue;
    public float SharedIntensityValue;
    public float SharedAppFormeValue;
    public float SharedAppLiquidValue;
    public float SharedTailleValue;
    public float SharedFormeValue;
    public float SharedDisparitionValue;
    [Header("OUT")]
    public float SmoothBlur;
    public float SmoothIntensity;
    public float SmoothAppForme;
    public float SmoothAppLiquid;
    public float SmoothRought;
    public float SmoothForme;
    public float SmoothTaille;
    public float SmoothDisparition;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SmoothBlur = Mathf.Lerp(SmoothBlur, SharedBlurValue, 0.2f);
        SmoothIntensity = Mathf.Lerp(SmoothIntensity,Mathf.SmoothStep(0,1, Mathf.Pow(SharedIntensityValue,2)), 0.05f)+AddSoundValue01;
        SmoothAppForme = Mathf.Lerp(SmoothAppForme, SharedAppFormeValue, 0.15f)+AddSoundValue02;
        SmoothAppLiquid = Mathf.Lerp(SmoothAppLiquid, SharedAppLiquidValue, 0.9f);
        SmoothTaille = Mathf.Lerp(SmoothTaille, SharedTailleValue, 0.02f);
        SmoothForme = Mathf.Lerp(SmoothForme, SharedFormeValue, 0.2f);
        SmoothDisparition = Mathf.Lerp(SmoothDisparition, SharedDisparitionValue, 0.3f);     
    }
}
