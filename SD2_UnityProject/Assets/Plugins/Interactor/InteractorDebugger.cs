using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Debugger that shows the current state of the InteractionMap
// Only work with orthographic camera

public class InteractorDebugger : MonoBehaviour
{
    [SerializeField] KeyCode toggleDebug = KeyCode.F1;
    [SerializeField] Material debugMat = null;

    InteractionMapProvider mapProvider;
    Camera cam;

    bool debugMode = false;

    GameObject debugQuad;

    void Start()
    {
        mapProvider = GetComponent<InteractionMapProvider>();
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleDebug))
        {
            debugMode = !debugMode;
            if (debugMode)
                SpawnDebugQuad();
            else
                DestroyDebugQuad();
        }
    }

    void SpawnDebugQuad()
    {
        Vector3 spawnPos = cam.transform.position + cam.transform.forward * 0.1f;
        debugQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        debugQuad.transform.position = spawnPos;
        debugQuad.transform.localScale = new Vector3((float)cam.pixelWidth / cam.pixelHeight, 1, 0);
        debugQuad.GetComponent<MeshRenderer>().material = debugMat;
        debugQuad.transform.SetParent(transform);
        debugQuad.name = "debugQuad";
        debugMat.mainTexture = mapProvider.InteractionMap;
    }

    void DestroyDebugQuad()
    {
        Destroy(debugQuad);
    }
}
