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
    public void AddIconIventaire(int id, int amount)
    {
        if (id < ListAllItems.instance.listeallItems.Count() && id >= 0)
        {
            int numbericons = 1 + amount / ListAllItems.instance.listeallItems[id].prefabIcon.GetComponent<Item>().ItemData.amountStockableMax;
            if (ListAllItems.instance.listeallItems[id].prefabIcon.GetComponent<Item>().ItemData.amountStockableMax == 1)
            {
                --numbericons;
            }
            int numberdone = 0;
            while (numberdone < numbericons)
            {
                bool asadded = false;
                GameObject icon = Instantiate(ListAllItems.instance.listeallItems[id].prefabIcon);
                if (numberdone == numbericons - 1)
                {
                    icon.GetComponent<Item>().amount = amount;
                }
                else
                {
                    icon.GetComponent<Item>().amount = icon.GetComponent<Item>().ItemData.amountStockableMax;
                }
                for (int i = 0; i < ListeObjets.Count; i++)
                {
                    if (ListeObjets[i].item == null)
                    {
                        ListeObjets[i].AddItemtoSlot(icon.GetComponent<Item>());
                        icon.GetComponent<Item>().gameObject.transform.SetParent(ListeObjets[i].transform);
                        ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                        ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        icon.GetComponent<Item>().parent = ListeObjets[i].gameObject;
                        ListeObjets[i].item.gameObject.GetComponent<Image>().sprite = ListAllItems.instance.listeallItems[id].prefabIcon.GetComponent<Item>().ItemData.iconImage;
                        icon.GetComponent<Item>().CreateTextAmount();
                        asadded = true;

                        break;
                    }
                    else
                    {
                        if (ListeObjets[i].item.GetComponent<Item>().ItemData.id == icon.GetComponent<Item>().ItemData.id &&
                            icon.GetComponent<Item>().ItemData.amountStockableMax >= ListeObjets[i].item.GetComponent<Item>().amount + amount)
                        {
                            ListeObjets[i].AddtwoItem(ListeObjets[i].item, icon.GetComponent<Item>());
                            ListeObjets[i].item.UpdateTextAmount();
                            asadded = true;
                            break;

                        }
                    }
                }
                if (!asadded)
                {
                    GameObject ObjectDroped = Instantiate(ListAllItems.instance.listeallItems[id].prefab3D, new Vector3(0, 10, 0), Quaternion.identity);
                    ObjectDroped.AddComponent<MeshCollider>();
                    ObjectDroped.GetComponent<MeshCollider>().convex = true;
                    ObjectDroped.AddComponent<Rigidbody>();
                    Debug.Log("Non Ajout�");
                    Destroy(icon);
                }
                else
                {
                    amount -= icon.GetComponent<Item>().ItemData.amountStockableMax;
                }
                numberdone++;
            }
        }
        else
        {
            Debug.LogError("Mauvais id");
        }
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
                ListeObjets[i].AddItemtoSlot(item.GetComponent<Item>());
                item.GetComponent<Item>().gameObject.transform.SetParent(ListeObjets[i].transform);
                ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                item.GetComponent<Item>().parent = ListeObjets[i].gameObject;
                ListeObjets[i].item.gameObject.GetComponent<Image>().sprite = item.GetComponent<Item>().ItemData.iconImage;
                item.GetComponent<Item>().CreateTextAmount();
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
                ListeObjets[i].AddItemtoSlot(itemnew.GetComponent<Item>());
                itemnew.GetComponent<Item>().gameObject.transform.SetParent(ListeObjets[i].transform);
                ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                itemnew.GetComponent<Item>().parent = ListeObjets[i].gameObject;
                ListeObjets[i].item.gameObject.GetComponent<Image>().sprite = itemnew.GetComponent<Item>().ItemData.iconImage;
                itemnew.GetComponent<Item>().CreateTextAmount();
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

