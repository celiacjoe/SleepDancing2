using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// Temporary interactor that "paint" on the InteractionMap from mouse data, using screen coordinate.
// Will be replace by something that paint on the map from camera data.



public class MouseInteractor : MonoBehaviour
{
    [SerializeField] ComputeShader PainterCS = null;
    [SerializeField] int interactionRadius = 20;
    [SerializeField] float interactionStrength = 0.1f;
    [SerializeField] float noiseFreqA = 5;
    [SerializeField] float noiseAmpA = 5;
    [SerializeField] float noiseFreqB = 5;
    [SerializeField] float noiseAmpB = 5;
    InteractionMapProvider interactionMap;
    int kMousePaint;




    void Start()
    {
        interactionMap = GetComponent<InteractionMapProvider>();
        kMousePaint = PainterCS.FindKernel("MouseInteraction");
        //PainterCS.SetTexture(kMousePaint, "interactionMap", interactionMap.InteractionMap);
    }



    void Update()
    {
        if (Input.GetMouseButton(0))
            Paint();
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
    }
}
