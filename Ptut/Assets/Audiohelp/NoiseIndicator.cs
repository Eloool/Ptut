using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseIndicator : MonoBehaviour
{
    private Transform playerTransform; 
    private Transform noiseSourceTransform; 
    private Camera mainCamera; 
    public RectTransform iconRect; 
    public RectTransform circleRect; 
    private void Update()
    {
        if (playerTransform == null || noiseSourceTransform == null || mainCamera == null || iconRect == null || circleRect == null)
            return;

        Vector3 noiseDirection = (noiseSourceTransform.position - playerTransform.position).normalized;

        Vector3 cameraRelativeDirection = mainCamera.transform.InverseTransformDirection(noiseDirection);

        float angle = Mathf.Atan2(cameraRelativeDirection.y, cameraRelativeDirection.x) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        float radius = circleRect.sizeDelta.x / 2f; 
        Vector2 clampedPosition = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * radius;

        iconRect.anchoredPosition = clampedPosition; 

        iconRect.rotation = Quaternion.Euler(0, 0, -angle-90); 
    }

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
