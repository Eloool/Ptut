using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AlertWorldCollider : MonoBehaviour
{
    // Référence au Canvas
    public GameObject canvasWorldCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Désactive le Canvas au démarrage
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

            // Optionnel : Masquer le Canvas après un délai
            StartCoroutine(HideCanvasAfterDelay(3f)); // 3 secondes
        }
    }

    // Coroutine pour masquer le Canvas après un délai
    private IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (canvasWorldCollider != null)
        {
            canvasWorldCollider.SetActive(false);
        }
    }
}

