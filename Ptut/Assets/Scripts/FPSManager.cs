using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour
{
    public Text fpsText;
    private float deltaTime = 0.0f;
    private float lastFPS = 0.0f;

    void Update()
    {
        if (Time.timeScale > 0)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            lastFPS = 1.0f / deltaTime;
        }

        if (fpsText != null)
        {
            fpsText.text = string.Format("{0:0.} FPS", lastFPS);
        }
    }
}
