using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventaire : InventoryBase
{
    private List<InventoryItem> ListeArmure;

    override public void Start()
    {
        ListeObjets = GetComponentsInChildren<InventoryItem>().ToList();
        foreach (InventoryItem item in ListeObjets)
        {
            item.gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<ListeItems>().Background;
        }
        ListeObjets.RemoveRange(ListeObjets.Count-4, 4);
        ListeArmure = GetComponentsInChildren<InventoryItem>().ToList();
        foreach (InventoryItem item in ListeArmure)
        {
            item.gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<ListeItems>().Background;
        }
        ListeArmure.RemoveRange(0, ListeObjets.Count);
        AddIconIventaire(4, 5);
        AddIconIventaire(2, 5);
        AddIconIventaire(0, 40);
    }
}

