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

    // Références aux scripts ou composants contrôlant la caméra et les mouvements du joueur
    public MonoBehaviour cameraController;
    public MonoBehaviour playerController;

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
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;
        GameIsPaused = false;

        // Réactiver le mouvement de la caméra
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        // Réactiver le mouvement du joueur
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Désactiver le mouvement de la caméra
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Désactiver le mouvement du joueur
        if (playerController != null)
        {
            playerController.enabled = false;
        }
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
