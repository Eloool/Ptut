using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeItems : MonoBehaviour
{
    public GameObject[] listeItems;

    private void Start()
    {
        for (int i = 0; i < listeItems.Length; i++)
        {
            listeItems[i].GetComponent<Item>().id= i;
        }
    }
}
