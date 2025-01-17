using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool inDeafMode;
    public bool inFullscreen;


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
}
