using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool inDeafMode;
    public bool inFullscreen;

    //public TMP_Dropdown qualityDropdown;


    
    public static int qualityIndex = 2;
    public TMP_Text qualityText;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else Destroy(gameObject);

    }

    private void Start()
    {
        inDeafMode = false;
        inFullscreen = true;
        Screen.fullScreen = inFullscreen;

        SetQuality(qualityIndex);

    }

    public void ToggleDeafMode()
    {
        inDeafMode = !inDeafMode;
    }

    public void ToggleFullscreen()
    {
        inFullscreen = !inFullscreen;
        if (inFullscreen)
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log($"Quality changed to: {QualitySettings.names[qualityIndex]}");

        QualitySettings.vSyncCount = 0;  // Disable V-Sync to allow unlimited FPS

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GameManager.qualityIndex = qualityIndex;
        //UpdateQualityText();
        
    }

    //private void UpdateQualityText()
    //{
    //    if (qualityText != null)
    //    {
    //        qualityText.text = $"{QualitySettings.names[qualityIndex]}"; // Update the text to show the quality name
    //    }
    //}
}
