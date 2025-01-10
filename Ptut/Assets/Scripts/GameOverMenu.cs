using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject CanvaGameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats.isDying)
        {
            StartCoroutine(activateCanva());
        }
    }

    IEnumerator activateCanva()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CanvaGameOver.SetActive(true);
        Time.timeScale = 0.2f;// met le time scale a 0.2 pour que le player die au ralenti

        // Attente de 5 secondes en temps réel
        yield return new WaitForSecondsRealtime(10);

        // Réglage du Time.timeScale à 0
        Time.timeScale = 0;
    }
}
