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
    public bool modesourd;

    private void Start()
    {
        // Configure l'AudioSource pour boucler
        if (audioSource != null)
        {
            audioSource.spatialBlend = 1f; // Son 3D
            audioSource.maxDistance = noiseRadius; // Distance maximale pour l'att�nuation
            audioSource.rolloffMode = AudioRolloffMode.Linear; // Att�nuation lin�aire
            audioSource.loop = true; // Active la boucle
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

        modesourd = GameManager.inDeafMode;
    }

    private void Update()
    {
        modesourd = GameManager.inDeafMode;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerTransform == null) return;

        // Calculer la distance entre le joueur et la source de bruit
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= noiseRadius)
        {
            EmitNoise();

            // D�marre ou continue le son si disponible
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            ClearNoiseIcon();

            // Arr�te le son lorsque le joueur quitte le radius
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void EmitNoise()
    {
        GameObject canvas = GameObject.Find("InventoryCanvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas introuvable !");
            return;
        }

        if (activeNoiseIcon == null && noiseIconPrefab != null && modesourd == true)
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
}
