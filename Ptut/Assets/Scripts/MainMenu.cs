using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject _LoadingScreen;
    public Image LoadingFill;


    public void Start()
    {
        Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        Time.timeScale = 0.0f;
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

}
