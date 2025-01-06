using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseRadius = 10f; // Rayon du bruit
    public GameObject noiseIconPrefab; // Pr�fab pour l'ic�ne visuelle
    private GameObject activeNoiseIcon; // Instance de l'ic�ne
    public AudioSource audioSource; // Source audio associ�e au bruit

    private void Start()
    {
        // Configure automatiquement le SphereCollider
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = noiseRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est un joueur ou un listener
        if (other.CompareTag("Player"))
        {
            Debug.Log("Joueur d�tect� !");
            EmitNoise();

            // Si l'audio source existe, d�marre le son
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Arr�te le son et d�truit l'ic�ne lorsque le joueur sort du radius
        if (other.CompareTag("Player"))
        {
            ClearNoiseIcon();

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void EmitNoise()
    {
        GameObject canvas = GameObject.Find("Canvas"); // Remplace "StatsCanvas" par le nom exact de ton Canvas.
        // Affiche l'ic�ne si elle n'existe pas encore
        if (activeNoiseIcon == null && noiseIconPrefab != null)
        {
            if (canvas != null)
            {
                activeNoiseIcon = Instantiate(noiseIconPrefab, canvas.transform);
                Debug.Log("Ic�ne ajout�e au Canvas");

                // Ajuste la position relative � l'�l�ment Canvas
                RectTransform rectTransform = activeNoiseIcon.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    // Position dans le Canvas (par exemple, centr�)
                    rectTransform.anchoredPosition = Vector2.zero;
                }
            }
            else
            {
                Debug.LogError("Canvas introuvable !");
            }
        }
    }

    public void ClearNoiseIcon()
    {
        if (activeNoiseIcon != null)
        {
            Destroy(activeNoiseIcon);
            activeNoiseIcon = null;
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, noiseRadius);
    }*/
}
