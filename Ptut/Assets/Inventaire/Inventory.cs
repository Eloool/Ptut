using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ToogleCanvas
{
    [System.Serializable]
    public class StarterItem
    {
        public ItemData item;
        public int amount;
    }
    public InventoryMenu inventaire;
    public ActionBar ActionBar;
    public Sprite Background;
    public GameObject DropSpots;
    public List<StarterItem> ItemsStarter;
    public CanvasAddingInventory addingInventory;

    public Transform DropPoint;
    public static Inventory instance;


    private void Start()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Debug.LogWarning("Une autre instance de ListeItems a été trouv�e et d�truite.");
        }
        
        inventaire.StartInventaire();
        ActionBar.StartInventaire();
        

        foreach (StarterItem item in ItemsStarter)
        {
            if (item.amount > 0 && item.item!=null)
            {
                GameObject gameObject = Instantiate(ListAllItems.instance.listeallItems[item.item.id].prefabIcon);
                gameObject.GetComponent<Item>().amount = item.amount;
                AddtoInventory(gameObject);
            }
        }
        ActionBar.Reload3DObjects();
        inventaire.ToogleCanDragitem(false);
        ActionBar.ToogleCanDragitem(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventaire.gameObject.activeInHierarchy)
            {
                CanvasController.instance.ShowCanvas(this);
            }
            else
            {
                GetComponentInChildren<ActionBar>().Reload3DObjects();
                CanvasController.instance.HideAllCanvases();
            }
        }
    }
    public void ToogleInventory(bool showArmor , bool active)
    {
        inventaire.gameObject.SetActive(active);
        inventaire.ShowArmor(showArmor);
        if (showArmor)
        {
            DropSpots.SetActive(inventaire.gameObject.activeInHierarchy);
        }
        else
        {
            DropSpots.SetActive(false);
        }
        ActionBar.ToogleCanDragitem(active);
        inventaire.ToogleCanDragitem(active);
    }
    public void AddtoInventory(GameObject item)
    {
        int amount = item.GetComponent<Item>().amount;
        bool wasaddedfully = ActionBar.AddIconIventaire(item);
        if (!wasaddedfully)
        {
            wasaddedfully = inventaire.AddIconIventaire(item);
        }
        else
        {
            ActionBar.Reload3DObjects();
        }
        if (!wasaddedfully)
        {
            while (!wasaddedfully)
            {
                wasaddedfully = ListAllItems.Create3DItem(item, DropPoint);
            }
        }
        else
        {
            addingInventory.AddCanvasItem(item.GetComponent<Item>(),amount);
        }
    }
    public void AddtoInventorybyItem3d(GameObject item3d)
    {
        GameObject icon = item3d.GetComponent<Item3d>().IconItem;
        icon.transform.SetParent(null);
        item3d.GetComponent<Item3d>().IconItem = null;
        AddtoInventory(icon);
    }

    public bool HasItem(ItemDataAndAmount item)
    {
        if (item.requiredItem.id  < 0 || item.requiredItem.id > ListAllItems.instance.listeallItems.Count)
        {
            Debug.LogError("L'indice pass� en param�tre est inferieur à 0 ou superieur à la longeur du tableau.");
            return false;
        }
        int countItem = 0;
        foreach (var invItem in inventaire.ListeObjets) 
        {
            if (invItem.item != null && invItem.item.ItemData.id == item.requiredItem.id)
            {
                countItem += invItem.item.amount;
                if (countItem >= item.amount)
                {
                    return true;
                }
            }
        }

        foreach (var actionBarItem in ActionBar.ListeObjets)
        {
            if (actionBarItem.item != null && actionBarItem.item.ItemData.id == item.requiredItem.id)
            {
                countItem += actionBarItem.item.amount;
                if (countItem >= item.amount)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public void DeleteItems(ItemDataAndAmount[] ids)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (!ActionBar.DeleteItem(ids[i].requiredItem.id, ids[i].amount))
            {
                inventaire.DeleteItem(ids[i].requiredItem.id, ids[i].amount);
            }
        }
    }
    public override void SetActiveCanvas(bool active)
    {
        if (active)
        {
            ToogleInventory(true, active);
        }
        else
        {
            ToogleInventory(false , active);
        }
    }

    public Item GetFirstItemWithType(ItemData.TypeItem item)
    {
        Item itemOut = ActionBar.GetFirstItemWithType(item);
        if (itemOut == null)
        {
            itemOut = inventaire.GetFirstItemWithType(item);
        }
        return itemOut;
    }
}
