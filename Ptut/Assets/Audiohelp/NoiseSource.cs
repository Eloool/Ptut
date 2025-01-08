using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseRadius = 10f; // Rayon du bruit
    public GameObject noiseIconPrefab; // Pr�fab pour l'ic�ne visuelle
    private GameObject activeNoiseIcon; // Instance de l'ic�ne
    public AudioSource audioSource; // Source audio associ�e au bruit

    private Transform playerTransform; // R�f�rence au joueur

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
            Debug.LogError("Aucun objet avec le tag 'Player' trouv� !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EmitNoise();

            // D�marre le son si disponible
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
            // Cr�e l'ic�ne et l'attache au Canvas
            activeNoiseIcon = Instantiate(noiseIconPrefab, canvas.transform);

            // Configure le NoiseIndicator pour suivre le joueur et la source
            NoiseIndicator indicator = activeNoiseIcon.GetComponent<NoiseIndicator>();
            if (indicator != null)
            {
                indicator.Initialize(playerTransform, transform, Camera.main); // Passe aussi la cam�ra ici
            }
            else
            {
                Debug.LogError("Le prefab d'ic�ne n'a pas de script NoiseIndicator !");
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
