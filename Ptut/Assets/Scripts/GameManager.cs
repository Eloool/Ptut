using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool inDeafMode;
    public bool inFullscreen;

    public TMP_Dropdown qualityDropdown;


    private void Start()
    {
        inDeafMode = false;
        inFullscreen = true;
        Screen.fullScreen = inFullscreen;
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

        //Screen.fullScreen = inFullscreen;
    }

    public void setQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
        Debug.Log($"Quality changed to: {QualitySettings.names[qualityindex]}");

        //if (qualityDropdown != null)
        //{
        //    qualityDropdown.value = qualityindex;
        //}
    }
}
