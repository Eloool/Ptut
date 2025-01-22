using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject _LoadingScreen;
    public Image LoadingFill;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int SceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneId);

        _LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressvalue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingFill.fillAmount = progressvalue;
            yield return null;
        }
    }
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadSceneAsync(1);
        LoadScene(1);
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
