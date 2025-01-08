using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorModeManager : MonoBehaviour
{
    public Material colorBlindMaterial; // Le mat�riau utilis� pour les effets daltoniens

    // Valeur de mode daltonien : 0 = Normal, 1 = Protanopie, 2 = Deuteranopie, 3 = Tritanopie
    private int currentMode = 0;

    void Update()
    {
        // Changer le mode de daltonien lorsque les touches sont appuy�es
        if (Input.GetKeyDown(KeyCode.F1)) // Touche 1 pour Protanopie
        {
            Debug.Log("1");
            SetColorMode(1);
        }
        else if (Input.GetKeyDown(KeyCode.F2)) // Touche 2 pour Deuteranopie
        {
            Debug.Log("2");
            SetColorMode(2);
        }
        else if (Input.GetKeyDown(KeyCode.F3)) // Touche 3 pour Tritanopie
        {
            Debug.Log("3");
            SetColorMode(3);
        }
        else if (Input.GetKeyDown(KeyCode.F4)) // Touche 0 pour r�initialiser � la normale
        {
            Debug.Log("4");
            SetColorMode(0);
        }
    }

    // Fonction pour changer le mode de daltonien en fonction de la touche appuy�e
    void SetColorMode(int mode)
    {
        // Mettre � jour la valeur de _ColorMode dans le mat�riau
        if (colorBlindMaterial != null)
        {
            colorBlindMaterial.SetFloat("_ColorMode", mode); // Change la valeur dans le shader
            currentMode = mode;
        }
    }
}
