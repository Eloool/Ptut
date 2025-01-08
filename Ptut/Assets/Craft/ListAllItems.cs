using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAllItems : MonoBehaviour
{
    [System.Serializable]
    public class iconand3d
    {
        public GameObject Icon;
        public GameObject Objet3d;
    }

    public List<iconand3d> listeallItems;
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
        foreach (iconand3d item in listeallItems)
        {
            if(item.Icon.GetComponent<Item>().ItemData == null)
            {
                Debug.LogWarning("Pas d'ItemData pour " + item.Icon.GetComponent<Item>());
            }
        }
        listeallItems.Sort(delegate (iconand3d x, iconand3d y)
        {
            return x.Icon.GetComponent<Item>().ItemData.id.CompareTo(y.Icon.GetComponent<Item>().ItemData.id);
        });


        int countingid = 0;
        for (int i = 0; i < listeallItems.Count; countingid++)
        {
            if (listeallItems[i].Icon.GetComponent<Item>().ItemData.id != countingid)
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
