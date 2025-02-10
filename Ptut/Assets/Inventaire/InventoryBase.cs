using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public abstract class InventoryBase : MonoBehaviour
{
    public List<InventoryItem> ListeObjets;
    virtual public void StartInventaire()
    {
        ListeObjets = GetComponentsInChildren<InventoryItem>().ToList();
        foreach (InventoryItem item in ListeObjets)
        {
            item.gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<Inventory>().Background;
        }
        ListeObjets.RemoveRange(ListeObjets.Count - 4, 4);
    }
    public bool AddIconIventaire(GameObject item)
    {
        if (item.GetComponent<Item>().amount == 0)
        {
            return false;
        }
        for (int i = 0; i < ListeObjets.Count; i++)
        {
            if (ListeObjets[i].item == null && item.GetComponent<Item>().amount <= item.GetComponent<Item>().ItemData.amountStockableMax && item.GetComponent<Item>().amount > 0)
            {
                ListeObjets[i].AddNewItemInSlot(item);
                return true;
            }
            else if (ListeObjets[i].item == null && item.GetComponent<Item>().amount > item.GetComponent<Item>().ItemData.amountStockableMax)
            {
                GameObject itemnew = Instantiate(item);
                if (itemnew.GetComponent<Item>().amount <= 0)
                {
                    break;
                }
                itemnew.GetComponent<Item>().amount = itemnew.GetComponent<Item>().ItemData.amountStockableMax;
                item.GetComponent<Item>().amount -= item.GetComponent<Item>().ItemData.amountStockableMax;
                ListeObjets[i].AddNewItemInSlot(itemnew);
            }
            else
            {
                if (ListeObjets[i].item.GetComponent<Item>().ItemData.id 
                    == item.GetComponent<Item>().ItemData.id)
                    //item.GetComponent<Item>().ItemData.amountStockableMax >= ListeObjets[i].item.GetComponent<Item>().amount + item.GetComponent<Item>().amount)
                {
                    ListeObjets[i].AddtwoItem(ListeObjets[i].item, item.GetComponent<Item>());
                    ListeObjets[i].item.UpdateTextAmount();
                    if (item.GetComponent<Item>().amount == 0)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool isfull()
    {
        foreach (var item in ListeObjets)
        {
            if (item.item == null)
            {
                return false;
            }
        }
        return true;
    }
    virtual public void ToogleCanDragitem(bool active)
    {
        foreach (var item in ListeObjets)
        {
            item.candragItem = active;
            if (item.item != null)
            {
                item.item.candragitem = active;
 
            }
        }
    }
    public bool DeleteItem(int id, int amount)
    {
        foreach (var item in ListeObjets)
        {
            if (item.item != null)
            {
                if (item.item.ItemData.id == id && item.item.amount == amount)
                {
                    Destroy(item.item.gameObject);
                    item.item = null;
                    return true;
                }
                else if (item.item.ItemData.id == id && item.item.amount < amount)
                {
                    amount -= item.item.amount;
                    Destroy(item.item.gameObject);
                    item.item = null;
                    

                }
                else if (item.item.ItemData.id == id && item.item.amount > amount)
                {
                    item.item.amount -= amount;
                    item.item.UpdateTextAmount();
                    return true;
                }
            }
        }
        return false;
    }

    public Item GetFirstItemWithType(ItemData.TypeItem typeItem)
    {
        foreach (var item in ListeObjets)
        {
            if (item.item != null)
            {
                if (item.item.ItemData.TypeOfItem == typeItem)
                {
                    return item.item;
                }
            }
        }
        return null;
    }
}

