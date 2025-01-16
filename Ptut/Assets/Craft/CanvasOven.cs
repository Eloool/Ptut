using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOven : ToogleCanvas
{
    public GameObject oven;
    public List<CookingData> cookingData;
    public static CanvasOven instance;
    public Oven lastOven;
    public OvenInventoryItem[] ListInventoryItem;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        ListInventoryItem = oven.GetComponentsInChildren<OvenInventoryItem>();
        foreach (var item in cookingData)
        {
            if (item.ItemGotbyCooking.amount> item.ItemGotbyCooking.requiredItem.amountStockableMax)
            {
                Debug.LogError("Les items données par le fours pour : " + item + " sont supérieurs à la capacité max du stack de l'item");
            }
        }
    }
    public void ToggleOven(Oven newOven)
    {
        if (!oven.activeInHierarchy)
        {
            CanvasController.instance.ShowCanvas(this);
            lastOven = newOven;
            lastOven.isOpen = true;
            oven.SetActive(true);
            Inventory.instance.ToogleInventory(false, true);
            Item[] ListOven = lastOven.GetComponentsInChildren<Item>();
            for (int i = 0; i < ListOven.Length; i++)
            {
                switch (ListOven[i].slot)
                {
                    case 0:
                        lastOven.Combustible = ListOven[i];
                        ListInventoryItem[ListOven[i].slot].SetItem(lastOven.Combustible.gameObject);
                        break;
                    case 1:
                        lastOven.Bruler = ListOven[i];
                        ListInventoryItem[ListOven[i].slot].SetItem(lastOven.Bruler.gameObject);
                        break;
                    case 2:
                        lastOven.Sortie = ListOven[i];
                        ListInventoryItem[ListOven[i].slot].SetItem(lastOven.Sortie.gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void CloseOven()
    {
        if (oven.activeInHierarchy)
        {
            for (int i = 0; i < ListInventoryItem.Length; i++)
            {
                GameObject gameObject = ListInventoryItem[i].DetachItem();
                if (gameObject != null)
                {
                    gameObject.GetComponent<Item>().slot = (int)ListInventoryItem[i].typeSlot;
                    gameObject.transform.SetParent(lastOven.transform);
                }
            }
            oven.SetActive(false);
            lastOven.isOpen = false;
            Inventory.instance.ToogleInventory(false, false);
        }
    }

    public CookingData GetCookingData(ItemData itemData)
    {
        foreach (var item in cookingData)
        {
            if (itemData == item.ItemsNeededforCooking.requiredItem)
            {
                return item;
            }
        }
        return null;
    }

    public override void SetActiveCanvas(bool active)
    {
        if (!active)
        {
            CloseOven();
        }
    }

}
