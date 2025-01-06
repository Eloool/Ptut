using UnityEngine;

public class CanvasAboveObject : MonoBehaviour
{
    public float heightOffset = 0.3f;

    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent;
        if (parentTransform != null)
        {
            Vector3 targetPosition = parentTransform.position + Vector3.up * heightOffset;
            transform.position = targetPosition;
        }
    }
}
