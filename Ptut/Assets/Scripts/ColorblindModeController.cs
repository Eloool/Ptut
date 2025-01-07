using UnityEngine;

public class ColorblindModeController : MonoBehaviour
{
    public Material protanopiaMaterial;
    public Material deuteranopiaMaterial;
    public Material tritanopiaMaterial;
    public Material normalMaterial;

    private Material currentMaterial;

    void Start()
    {
        currentMaterial = tritanopiaMaterial;
    }

    public void SetProtanopia()
    {
        currentMaterial = protanopiaMaterial;
    }

    public void SetDeuteranopia()
    {
        currentMaterial = deuteranopiaMaterial;
    }

    public void SetTritanopia()
    {
        currentMaterial = tritanopiaMaterial;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (currentMaterial != null)
        {
            Graphics.Blit(source, destination, currentMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
