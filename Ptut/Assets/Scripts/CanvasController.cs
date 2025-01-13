using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public ToogleCanvas[] canvases;

    public static CanvasController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowCanvas(ToogleCanvas targetCanvas)
    {
        foreach (ToogleCanvas canvas in canvases)
        {
            canvas.SetActiveCanvas(canvas == targetCanvas);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ShowCanvasByIndex(int index)
    {
        if (index >= 0 && index < canvases.Length)
        {
            ShowCanvas(canvases[index]);
        }
        else
        {
            Debug.LogError("Index out of range for canvases array.");
        }
    }

    public void HideAllCanvases()
    {
        foreach (ToogleCanvas canvas in canvases)
        {
            canvas.SetActiveCanvas(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}