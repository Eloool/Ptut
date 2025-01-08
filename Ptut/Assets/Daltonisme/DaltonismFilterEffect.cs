using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DaltonismFilterEffect : MonoBehaviour
{
    public Material daltonismMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (daltonismMaterial != null)
        {
            Debug.Log("Applying Daltonism Filter");
            Graphics.Blit(src, dest, daltonismMaterial);
        }
        else
        {
            Debug.Log("Daltonism material is not assigned!");
            Graphics.Blit(src, dest);
        }
    }
}
