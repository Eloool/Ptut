using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private int playerLayer;

    private void Start()
    {
        // Récupère l'index du layer "Player"
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le trigger est sur le layer "Player"
        if (other.gameObject.layer == playerLayer)
        {
            // Téléporte l'objet vers la destination
            other.transform.position = destination.transform.position;
        }
    }
}