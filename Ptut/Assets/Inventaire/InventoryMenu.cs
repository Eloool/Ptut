using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
//using static UnityEditor.Progress;

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
        ArmorBehaviour.instance.DeloadAllArmor();
        foreach (InventoryItem item in ListeArmure)
        {
            if (item.item != null)
            {
                PlayerStats.instance.armorResistance += item.item.GetStat<ArmorStat>().defense;
                ArmorBehaviour.instance.LoadArmor(item.item.ItemData.id);
            }
        }
        
    }

    private bool isChangingState = false;

    public void ShowArmor(bool show)
    {
        if (isChangingState)
        {
            Debug.LogWarning("ShowArmor est déjà en cours d'exécution.");
            return;
        }

        isChangingState = true;

        try
        {
            Debug.Log($"ShowArmor called with show = {show}");

            if (ListeArmure == null || ListeArmure.Count == 0)
            {
                Debug.LogWarning("ListeArmure est vide ou non initialisée.");
                return;
            }

            GameObject parentObject = ListeArmure[0].gameObject.transform.parent?.gameObject;
            if (parentObject == null)
            {
                Debug.LogWarning("Le parent de l'armure n'existe pas.");
                return;
            }

            if (parentObject.activeSelf != show)
            {
                Debug.Log($"Changing parentObject active state to: {show}");
                parentObject.SetActive(show);
            }
            else
            {
                Debug.LogWarning("Le parent est déjà dans l'état demandé.");
            }
        }
        finally
        {
            isChangingState = false;
        }
    }


}

