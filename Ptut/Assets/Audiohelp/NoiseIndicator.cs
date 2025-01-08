using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseIndicator : MonoBehaviour
{
    private Transform playerTransform; // Référence au joueur
    private Transform noiseSourceTransform; // Référence à la source du bruit
    private Camera mainCamera; // Référence à la caméra
    public RectTransform iconRect; // RectTransform de l'icône
    public RectTransform circleRect; // RectTransform du cercle (parent de l'icône)
    private void Update()
    {
        if (playerTransform == null || noiseSourceTransform == null || mainCamera == null || iconRect == null || circleRect == null)
            return;

        // 1. Calcul de la direction entre le joueur et la source du bruit
        Vector3 noiseDirection = (noiseSourceTransform.position - playerTransform.position).normalized;

        // 2. Conversion de la direction dans l'espace de la caméra
        Vector3 cameraRelativeDirection = mainCamera.transform.InverseTransformDirection(noiseDirection);

        // 3. Calcul de l'angle en degrés dans l'espace caméra
        float angle = Mathf.Atan2(cameraRelativeDirection.y, cameraRelativeDirection.x) * Mathf.Rad2Deg;

        // 4. Normalisation de l'angle entre 0 et 360 degrés
        if (angle < 0)
            angle += 360;

        // 5. Positionner l'icône sur le cercle (en utilisant l'angle)
        float radius = circleRect.sizeDelta.x / 2f; // Rayon du cercle en pixels
        Vector2 clampedPosition = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * radius;

        iconRect.anchoredPosition = clampedPosition; // Place l'icône sur le cercle

        // 6. Faire tourner l'icône pour pointer vers le bruit
        iconRect.rotation = Quaternion.Euler(0, 0, -angle-90); // Rotation de l'icône pour qu'elle pointe correctement
    }

    // Méthode d'initialisation
    public void Initialize(Transform player, Transform noiseSource, Camera camera)
    {
        playerTransform = player;
        noiseSourceTransform = noiseSource;
        mainCamera = camera;

        // Trouver les RectTransform si nécessaire
        if (iconRect == null)
            iconRect = GetComponent<RectTransform>();
    }
}
