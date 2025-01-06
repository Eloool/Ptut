using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseIndicator : MonoBehaviour
{
    private Transform playerTransform; // Référence au joueur
    private Transform noiseSourceTransform; // Référence à la source de bruit
    private Transform arrowTransform; // Référence à l'objet flèche

    public Camera uiCamera; // Caméra utilisée pour afficher le Canvas

    public void Initialize(Transform player, Transform noiseSource, Camera camera)
    {
        playerTransform = player;
        noiseSourceTransform = noiseSource;
        uiCamera = camera;

        // Trouve la flèche dans l'icône
        arrowTransform = transform.Find("Arrow");
        if (arrowTransform == null)
        {
            Debug.LogError("Flèche introuvable dans le NoiseIndicator !");
        }
    }

    private void Update()
    {
        if (arrowTransform != null && playerTransform != null && noiseSourceTransform != null && uiCamera != null)
        {
            // Position du bruit et du joueur dans le monde
            Vector3 playerPosition = playerTransform.position;
            Vector3 noisePosition = noiseSourceTransform.position;

            // Calcule la direction bruit → joueur dans le plan horizontal (X, Z)
            Vector3 worldDirection = (noisePosition - playerPosition).normalized;
            worldDirection.y = 0; // Ignore la composante Y (verticale)

            // Transforme la direction en coordonnées locales à la caméra
            Vector3 localDirection = uiCamera.transform.InverseTransformDirection(worldDirection);

            // Calcule l'angle en 2D (Z-axis pour les rotations Canvas)
            float angle = Mathf.Atan2(localDirection.z, localDirection.x) * Mathf.Rad2Deg;

            // Applique l'angle à la flèche
            arrowTransform.localRotation = Quaternion.Euler(0, 0, -angle); // Note : signe inversé pour correspondre à l'UI
        }
    }
}
