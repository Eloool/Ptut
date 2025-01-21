using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public ToogleCanvas[] canvases;
    public static CanvasController instance;

    public Camera Camera;

    // R�f�rences aux scripts ou composants contr�lant la cam�ra et le mouvement du joueur
    public MonoBehaviour cameraController;
    

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

        // D�sactiver le mouvement de la cam�ra et du joueur
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
        if (!(targetCanvas is GameOverMenu cast))
        {
            Time.timeScale = 0f;
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

        // R�activer le mouvement de la cam�ra et du joueur
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
        Time.timeScale = 1.0f;
        
    }
}
