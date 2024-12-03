using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle easyToggle; 
    /*public Toggle mediumToggle; 
    public Toggle hardToggle;
    public Toggle personalizedToggle;*/

    private void Start()
    {
        // Initialiser l'état des toggles
        var difficulty = DifficultyManager.Instance.CurrentDifficulty;
        easyToggle.isOn = (difficulty == DifficultyManager.DifficultyLevel.Easy);
        /*mediumToggle.isOn = (difficulty == DifficultyManager.DifficultyLevel.Medium);
        hardToggle.isOn = (difficulty == DifficultyManager.DifficultyLevel.Hard);*/
    }

    public void OnEasyToggleChanged(bool isOn)
    {
        if (isOn) DifficultyManager.Instance.SetDifficulty(DifficultyManager.DifficultyLevel.Easy);
        Debug.Log("Facile");
    }

    /*public void OnMediumToggleChanged(bool isOn)
    {
        if (isOn) DifficultyManager.Instance.SetDifficulty(DifficultyManager.DifficultyLevel.Medium);
        Debug.Log("Moyen");
    }

    public void OnHardToggleChanged(bool isOn)
    {
        if (isOn) DifficultyManager.Instance.SetDifficulty(DifficultyManager.DifficultyLevel.Hard);
        Debug.Log("Difficile");
    }

    public void OnPersonalizedToggleChanged(bool isOn)
    {
        if (isOn) DifficultyManager.Instance.SetDifficulty(DifficultyManager.DifficultyLevel.Personalized);
        Debug.Log("Personnalisé");
    }*/
}
