using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AlertWorldCollider : MonoBehaviour
{
    // R�f�rence au Canvas
    public GameObject canvasWorldCollider;

    // Start is called before the first frame update
    void Start()
    {
        // D�sactive le Canvas au d�marrage
        if (canvasWorldCollider != null)
        {
            canvasWorldCollider.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Affiche le Canvas
        if (canvasWorldCollider != null)
        {
            canvasWorldCollider.SetActive(true);

            // Optionnel : Masquer le Canvas apr�s un d�lai
            StartCoroutine(HideCanvasAfterDelay(3f)); // 3 secondes
        }
    }

    // Coroutine pour masquer le Canvas apr�s un d�lai
    private IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (canvasWorldCollider != null)
        {
            canvasWorldCollider.SetActive(false);
        }
    }
}

