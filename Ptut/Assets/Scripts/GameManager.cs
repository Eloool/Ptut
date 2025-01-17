using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool modeSourd;
    public Button buttonOn;  // Bouton pour activer le mode sourd
    public Button buttonOff; // Bouton pour désactiver le mode sourd



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateButtonColors()
    {
        if (modeSourd)
        {
            SetButtonColor(buttonOn, Color.green);  // Active en vert
            SetButtonColor(buttonOff, Color.white); // Désactive en blanc
        }
        else
        {
            SetButtonColor(buttonOn, Color.white);  // Désactive en blanc
            SetButtonColor(buttonOff, Color.red); // Active en vert
        }
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color; // Couleur au survol
        colors.pressedColor = color;    // Couleur quand le bouton est pressé
        colors.selectedColor = color;  // Couleur quand le bouton est sélectionné
        button.colors = colors;
    }
    public void SetModeSourdOn()
    {
        modeSourd = true;
        UpdateButtonColors();
    }
    public void SetModeSourdOff()
    {
        modeSourd = false;
        UpdateButtonColors();
    }
}
