using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Entry point of all effects. The InteractionMap is a map representing the spots of interaction.
// Different modules can "paint" on this map using different input data (see MouseInteractor).



public class InteractionMapProvider : MonoBehaviour
{
    [SerializeField] ComputeShader PainterCS = null;
    [SerializeField, Range(0f, 1f)] float screenToMapRatio = 0.5f; // ratio between the screen resolution and the InteractionMap resolution
    [SerializeField] float interactionDecay = 0.01f;



    [SerializeField] RenderTexture interactionMap = null;




    public RenderTexture InteractionMap
    {
        get
        {
            return interactionMap;
        }
    }



    public float ScreenToMapRatio => screenToMapRatio;



    int resX;
    int resY;
    int threadGroupX;
    int threadGroupY;
    int threadGroupZ = 1;



    int kTick;



    public void AfterSceneChange()
    {
        ResetInteractionMap();
    }



    void Awake()
    {
        ResetInteractionMap();
        kTick = PainterCS.FindKernel("Tick");
    }



    void Update()
    {
        if (interactionMap == null)
            return;

        PainterCS.SetFloat("decay", interactionDecay * Time.deltaTime);
        Paint(PainterCS, kTick);
    }



    internal void Paint(ComputeShader CS, int kernel)
    {
        PainterCS.SetTexture(kernel, "interactionMap", interactionMap);
        CS.Dispatch(kernel, threadGroupX, threadGroupY, threadGroupZ);
    }



    void ResetInteractionMap()
    {
        Camera cam = FindObjectOfType<Camera>();
        if (cam == null)
            return;



        if (interactionMap != null)
            interactionMap.Release();



        resX = (int)(cam.pixelWidth * screenToMapRatio);
        resY = (int)(cam.pixelHeight * screenToMapRatio);



        interactionMap = new RenderTexture(resX, resY, 0, RenderTextureFormat.RG16);
        interactionMap.enableRandomWrite = true;
        interactionMap.Create();



        threadGroupX = Mathf.CeilToInt(resX / 8f);
        threadGroupY = Mathf.CeilToInt(resY / 8f);
    }
}