using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryMenu : InventoryBase
{
    private List<InventoryItem> ListeArmure;

    override public void StartInventaire()
    {
        base.StartInventaire();
        ListeArmure = GetComponentsInChildren<InventoryItem>().ToList();
        foreach (InventoryItem item in ListeArmure)
        {
            item.gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<Inventory>().Background;
        }
        ListeArmure.RemoveRange(0, ListeObjets.Count);
    }

    public void ReloadArmor()
    {
        PlayerStats.instance.armorResistance = 0;
        foreach (InventoryItem item in ListeArmure)
        {
            if (item.item != null)
            {
                PlayerStats.instance.armorResistance += item.item.GetStat<ArmorStat>().defense;
            }
        }
        
    }
    public void ShowArmor(bool show)
    {
        ListeArmure[0].gameObject.transform.parent.gameObject.SetActive(show);
    }
}

