using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans le trigger est le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Vous �tes proche de la pierre !");
        }
    }
}
