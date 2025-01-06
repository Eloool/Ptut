using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseRadius = 10f; // Rayon du bruit
    public GameObject noiseIconPrefab; // Préfab pour l'icône visuelle
    private GameObject activeNoiseIcon; // Instance de l'icône
    public AudioSource audioSource; // Source audio associée au bruit

    private Transform playerTransform; // Référence au joueur

    private void Start()
    {
        // Configure automatiquement le SphereCollider
        SphereCollider collider = GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.isTrigger = true;
            collider.radius = noiseRadius;
        }

        // Trouve le joueur
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'Player' trouvé !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EmitNoise();

            // Démarre le son si disponible
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
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
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas introuvable !");
            return;
        }

        if (activeNoiseIcon == null && noiseIconPrefab != null)
        {
            // Crée l'icône et l'attache au Canvas
            activeNoiseIcon = Instantiate(noiseIconPrefab, canvas.transform);

            // Configure le NoiseIndicator pour suivre le joueur et la source
            NoiseIndicator indicator = activeNoiseIcon.GetComponent<NoiseIndicator>();
            if (indicator != null)
            {
                indicator.Initialize(playerTransform, transform, Camera.main); // Passe aussi la caméra ici
            }
            else
            {
                Debug.LogError("Le prefab d'icône n'a pas de script NoiseIndicator !");
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
        Gizmos.DrawWireSphere(transform.position, noiseRadius * 2f);
    }*/
}
