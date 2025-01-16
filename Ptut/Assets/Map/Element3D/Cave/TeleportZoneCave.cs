using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private int playerLayer;

    private void Start()
    {
        // R�cup�re l'index du layer "Player"
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans le trigger est sur le layer "Player"
        if (other.gameObject.layer == playerLayer)
        {
            // T�l�porte l'objet vers la destination
            other.transform.position = destination.transform.position;
        }
    }
}