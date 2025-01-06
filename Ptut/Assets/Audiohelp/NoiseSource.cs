using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseRadius = 10f; // Rayon du bruit
    public GameObject noiseIconPrefab; // Préfab pour l'icône visuelle
    private GameObject activeNoiseIcon; // Instance de l'icône
    public AudioSource audioSource; // Source audio associée au bruit

    private void Start()
    {
        // Configure automatiquement le SphereCollider
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = noiseRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est un joueur ou un listener
        if (other.CompareTag("Player"))
        {
            Debug.Log("Joueur détecté !");
            EmitNoise();

            // Si l'audio source existe, démarre le son
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Arrête le son et détruit l'icône lorsque le joueur sort du radius
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
        // Affiche l'icône si elle n'existe pas encore
        if (activeNoiseIcon == null && noiseIconPrefab != null)
        {
            if (canvas != null)
            {
                activeNoiseIcon = Instantiate(noiseIconPrefab, canvas.transform);
                Debug.Log("Icône ajoutée au Canvas");

                // Ajuste la position relative à l'élément Canvas
                RectTransform rectTransform = activeNoiseIcon.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    // Position dans le Canvas (par exemple, centré)
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
