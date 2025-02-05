using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenu : ToogleCanvas
{
    public UnityEngine.UI.Image background;
    public float alpha;
    private Color color;
    private bool alphaUp;
    private int total;
    PlayerStats stats;
    public GameObject gameMenu;


    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        alpha = 0.0f;
        color = background.color;
        alphaUp = true;
        total = 0;

    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //    if (stats.isDying)
    //    {
    //        CanvasController.instance.ShowCanvas(canvaGO);
    //    }
    //}

    IEnumerator activateCanva()
    {
        gameMenu.SetActive(true);
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.2f;// met le time scale a 0.2 pour que le player die au ralenti

        // Attente de 5 secondes en temps r�el
        yield return new WaitForSecondsRealtime(6);

        // R�glage du Time.timeScale � 0
        Time.timeScale = 0;
    }

    public override void SetActiveCanvas(bool active)
    {
        if (active)
        {
            StartCoroutine(activateCanva());

            color.a = Mathf.Clamp01(alpha);
            background.color = color;

            if (alpha < 0) alphaUp = true;
            else if (alpha > 1)
            {
                alphaUp = false;
                total++;
            }

            if (alphaUp) alpha += Time.unscaledDeltaTime;
            else if (!alphaUp && total < 3) alpha -= Time.unscaledDeltaTime;

            CanvasController.instance.gameObject.SetActive(false);
        }
    }
}
