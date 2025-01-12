using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : ToogleCanvas
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                CanvasController.instance.ShowCanvas(this);
            }
            else
            {
                CanvasController.instance.HideAllCanvases();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if(Time.timeScale != 1f)
            Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public override void SetActiveCanvas(bool active)
    {
        GameIsPaused = !active;
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
