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
    public float SharedTailleValue;
    public float SharedFormeValue;
    public float SharedDisparitionValue;
    [Header("OUT")]
    public float SmoothBlur;
    public float SmoothIntensity;
    public float SmoothAppForme;
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
        SmoothBlur = Mathf.Lerp(SmoothBlur, SharedBlurValue, 0.75f);
        SmoothIntensity = Mathf.Lerp(SmoothIntensity, SharedIntensityValue, 0.5f)+AddSoundValue01;
        SmoothAppForme = Mathf.Lerp(SmoothAppForme, SharedAppFormeValue, 0.6f)+AddSoundValue02;
        SmoothTaille = Mathf.Lerp(SmoothTaille, SharedTailleValue, 0.05f);
        SmoothForme = Mathf.Lerp(SmoothForme, SharedFormeValue, 0.5f);
        SmoothDisparition = Mathf.Lerp(SmoothDisparition, SharedDisparitionValue, 0.5f);     
    }
}
