using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseRadius = 10f; // Rayon du bruit
    public GameObject noiseIconPrefab; // Préfab pour l'icône visuelle
    private GameObject activeNoiseIcon; // Instance de l'icône
    public AudioSource audioSource; // Source audio associée au bruit
    public GameObject CanvasAffichage;

    private Transform playerTransform; // Référence au joueur
    public bool modesourd;

    private void Start()
    {
        // Configure l'AudioSource pour boucler
        if (audioSource != null)
        {
            audioSource.spatialBlend = 1f; // Son 3D
            audioSource.maxDistance = noiseRadius; // Distance maximale pour l'atténuation
            audioSource.rolloffMode = AudioRolloffMode.Linear; // Atténuation linéaire
            audioSource.loop = true; // Active la boucle
            audioSource.ignoreListenerPause = false; // Respecte le Time.timeScale
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

        modesourd = GameManager.inDeafMode;
    }

    private void Update()
    {
        modesourd = GameManager.inDeafMode;
        if (playerTransform == null) return;

        // Arrêter tous les sons si le jeu est en pause
        if (Time.timeScale == 0)
        {
            AudioListener.pause = true; // Met en pause tous les sons
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            return;
        }
        else
        {
            AudioListener.pause = false; // Relance le son global
        }

        // Calculer la distance entre le joueur et la source de bruit
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= noiseRadius)
        {
            EmitNoise();

            // Démarre ou continue le son si disponible
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            ClearNoiseIcon();

            // Arrête le son lorsque le joueur quitte le radius
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }


    public void EmitNoise()
    {
        //GameObject canvas = GameObject.Find("InventoryCanvas");
        //if (canvas == null)
        //{
        //    Debug.LogError("Canvas introuvable !");
        //    return;
        //}

        if (activeNoiseIcon == null && noiseIconPrefab != null && modesourd == true)
        {
            // Crée l'icône et l'attache au Canvas
            activeNoiseIcon = Instantiate(noiseIconPrefab, CanvasAffichage.transform);

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

    private void OnDestroy()
    {
        ClearNoiseIcon();
    }

}
