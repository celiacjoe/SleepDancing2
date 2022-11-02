using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Binder between InteractionMap and visual effects

[RequireComponent(typeof(VisualEffect))]
public class VFXInteractionBinder : MonoBehaviour
{
    [SerializeField] string texturePropertyName = "InteractionMap";

    IEnumerator Start()
    {
        VisualEffect vfx = GetComponent<VisualEffect>();
        InteractionMapProvider mapProvider = FindObjectOfType<InteractionMapProvider>();
        if(mapProvider == null)
        {
            Debug.LogError("No InteractionMapProvider found in scene");
            yield break;
        }

        while (mapProvider.InteractionMap == null)
            yield return null;

        vfx.SetTexture(texturePropertyName, mapProvider.InteractionMap);


    }
}
