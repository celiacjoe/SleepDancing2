using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


// Temporary interactor that "paint" on the InteractionMap from mouse data, using screen coordinate. 
// Will be replace by something that paint on the map from camera data.

public class MouseInteractor_Celia : MonoBehaviour
{
    [SerializeField] ComputeShader PainterCS = null;
    [SerializeField] int interactionRadius = 20;
    [SerializeField] float interactionStrength = 0.1f;
    [SerializeField] float noiseFreqA = 5;
    [SerializeField] float noiseAmpA = 5;
    [SerializeField] float noiseFreqB = 5;
    [SerializeField] float noiseAmpB = 5;

    InteractionMapProvider interactionMap;
    public VisualEffect VFX;
    public int ValueX;
    public int ValueY;
    int kMousePaint;



    IEnumerator Start()
    {
        interactionMap = GetComponent<InteractionMapProvider>();
        while (interactionMap.InteractionMap == null)
            yield return null;
        kMousePaint = PainterCS.FindKernel("MouseInteraction");
        PainterCS.SetTexture(kMousePaint, "interactionMap", interactionMap.InteractionMap);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Paint();
            VFX.GetFloat("MouseX");
            VFX.SetFloat("MouseX", ValueX);
            VFX.GetFloat("MouseY");
            VFX.SetFloat("MouseY", ValueY);
        }
        else
        {
            VFX.GetFloat("MouseX");
            VFX.SetFloat("MouseX", -50);
            VFX.GetFloat("MouseY");
            VFX.SetFloat("MouseY", -50);
        }

    }

    private void Paint()
    {
        Vector3 screenPos = Input.mousePosition;
        int texPosX = Mathf.CeilToInt(screenPos.x * interactionMap.ScreenToMapRatio);
        int texPosY = Mathf.CeilToInt(screenPos.y * interactionMap.ScreenToMapRatio);
        PainterCS.SetFloat("noiseFreqA", noiseFreqA);
        PainterCS.SetFloat("noiseAmpA", noiseAmpA);
        PainterCS.SetFloat("noiseFreqB", noiseFreqB);
        PainterCS.SetFloat("noiseAmpB", noiseAmpB);
        PainterCS.SetInt("texPosX", texPosX);
        PainterCS.SetInt("texPosY", texPosY);
        PainterCS.SetInt("interactionRadius", interactionRadius);
        PainterCS.SetFloat("interactionStrength", interactionStrength * Time.deltaTime);
        interactionMap.Paint(PainterCS, kMousePaint);
        ValueX = texPosX;
        ValueY = texPosY;
    }
}
