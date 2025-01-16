using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverCanva;
    public GameObject InventoryCanva;
    public GameObject StatsCanva;
    public UnityEngine.UI.Image background;
    public float alpha;
    private Color color;
    private bool alphaUp;
    private int total;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.0f;
        color = background.color;
        alphaUp = true;
        total = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats.isDying)
        {
            StartCoroutine(activateCanva());

            color.a = Mathf.Clamp01(alpha);
            //background.tintColor = color;
            background.color = color;

            if (alpha < 0) alphaUp = true;
            else if (alpha > 1)
            {
                alphaUp = false;
                total++;
            }

            //if (alpha > 0) alphaDown = true;

            if (alphaUp) alpha += Time.unscaledDeltaTime;
            else if (!alphaUp && total < 3) alpha -= Time.unscaledDeltaTime;
        }
    }

    IEnumerator activateCanva()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        InventoryCanva.SetActive(false);
        StatsCanva.SetActive(false);
        GameOverCanva.SetActive(true);
        Time.timeScale = 0.2f;// met le time scale a 0.2 pour que le player die au ralenti

        // Attente de 5 secondes en temps réel
        yield return new WaitForSecondsRealtime(10);

        // Réglage du Time.timeScale à 0
        Time.timeScale = 0;
    }
}
