using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAllItems : MonoBehaviour
{
    public List<ItemData> listeallItems;
    public static ListAllItems instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Initialisation si instance n'existe pas
            Debug.Log("ListAllItems.instance a �t� initialis�.");
        }
        else
        {
            Debug.LogWarning("Une autre instance de ListeItems a �t� trouv�e et d�truite.");
        }
        foreach (ItemData item in listeallItems)
        {
            if(item.prefabIcon.GetComponent<Item>().ItemData == null)
            {
                Debug.LogWarning("Pas d'ItemData pour " + item.prefabIcon.GetComponent<Item>());
            }
        }
        listeallItems.Sort(delegate (ItemData x, ItemData y)
        {
            return x.prefabIcon.GetComponent<Item>().ItemData.id.CompareTo(y.prefabIcon.GetComponent<Item>().ItemData.id);
        });


        int countingid = 0;
        for (int i = 0; i < listeallItems.Count; countingid++)
        {
            if (listeallItems[i].prefabIcon.GetComponent<Item>().ItemData.id != countingid)
            {
                Debug.LogError("Pas d'items avec l'id" + countingid);
            }
            else
            {
                i++;
            }
        }
    }
}
