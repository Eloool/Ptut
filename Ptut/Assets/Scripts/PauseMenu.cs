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
    public GameObject Settings;
    public GameObject Help;

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
        Settings.SetActive(false);
        Help.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Inventory.instance.ActionBar.SetCanScroll(false);
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; // quits the game (temporary code)
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
