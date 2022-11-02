
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerStroke : MonoBehaviour
{
    [SerializeField] ComputeShader PainterCS = null;[SerializeField, Range(0f, 9f)] float fill = 0;

    InteractionMapProvider interactionMap; int kFill;

    void Start() { interactionMap = GetComponent<InteractionMapProvider>(); kFill = PainterCS.FindKernel("Fill"); }

    private void Update() { Paint(); }

    private void Paint() { PainterCS.SetFloat("fill", fill); interactionMap.Paint(PainterCS, kFill); }
}

