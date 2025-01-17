using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject difficulty;
    public GameObject mainMenu;

    public void ShowDifficulty()
    {
        difficulty.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(1);
    }

    public void goHome()
    {
        Debug.Log("Ca passe par la !");
        SceneManager.LoadSceneAsync(0);
    }
    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; // quits the game (temporary code)
    }

    public void Start()
    {
        Time.timeScale = 0f;
    }
}
