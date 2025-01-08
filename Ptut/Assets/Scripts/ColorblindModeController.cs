using UnityEngine;
using UnityEngine.UI;

public class ColorblindModeController : MonoBehaviour
{
    public Material protanopiaMaterial;
    public Material deuteranopiaMaterial;
    public Material tritanopiaMaterial;
    public Material normalMaterial;

    private Material currentMaterial;

    void Start()
    {
        currentMaterial = normalMaterial;
    }

    public void SetMode(int mode)
    {
        // 0 = Normal, 1 = Protanopia, 2 = Deuteranopia, 3 = Tritanopia
        switch (mode)
        {
            case 1:
                currentMaterial = protanopiaMaterial;
                break;
            case 2:
                currentMaterial = deuteranopiaMaterial;
                break;
            case 3:
                currentMaterial = tritanopiaMaterial;
                break;
            default:
                currentMaterial = normalMaterial;
                break;
        }
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
