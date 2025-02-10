using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class InventoryItem : MonoBehaviour,IDropHandler
{
    public Item item;
    public RectTransform Position;
    public bool candragItem=true;
    private RectTransform RectParent;
    private bool canSwap = true;

    private void Start()
    {
        Position = GetComponent<RectTransform>();
        Position.sizeDelta = new Vector2(125, 125);
        RectParent = transform.parent.GetComponent<RectTransform>();
    }
    public void AddItemtoSlot(Item iteme)
    {
        item = iteme;
    }
    virtual public void OnDrop(PointerEventData eventData)
    {
        canSwap = true;
        bool fromArmor = false;
        if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<ArmureSlot>() != null)
        {
            fromArmor = true;
        }
        if (candragItem)
        {
            if (CanvasOven.instance.oven.activeInHierarchy)
            {
                if (!CanvasOven.instance.lastOven.iscooking && eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>() != null)
                {
                    if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>().typeSlot == OvenInventoryItem.TypeOvenSlot.Sortie && gameObject.GetComponent<OvenInventoryItem>() != null)
                    {
                        canSwap = false;
                    }
                    else if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>().typeSlot == OvenInventoryItem.TypeOvenSlot.Sortie && item != null)
                    {
                        canSwap = false;
                    }
                }
            }
            if (canSwap)
            {
                if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>() != null)
                {
                    if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>().typeSlot == OvenInventoryItem.TypeOvenSlot.Sortie && item == null)
                    {
                        CanvasOven.instance.lastOven.SetSortie(null);
                    }
                    else if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>().typeSlot == OvenInventoryItem.TypeOvenSlot.Combustible)
                    {
                        CanvasOven.instance.lastOven.SetCombustible(item);
                    }
                    else if (eventData.pointerDrag.GetComponent<Item>().parent.GetComponent<OvenInventoryItem>().typeSlot == OvenInventoryItem.TypeOvenSlot.Cooking)
                    {
                        CanvasOven.instance.lastOven.SetBruler(item);
                    }
                }
                DropItem(eventData.pointerDrag.GetComponent<Item>());
            }
            if(fromArmor)
                Inventory.instance.inventaire.ReloadArmor();
        }
        
        canSwap = true;
    }
    public void AddtwoItem(Item item1, Item item2)
    {
        int numberofiteminexcess = 0;
        item1.amount += item2.amount;
        if (item1.amount > item1.ItemData.amountStockableMax)
        {
            numberofiteminexcess = item1.amount - item1.ItemData.amountStockableMax;
            item1.amount -= numberofiteminexcess;
            item1.UpdateTextAmount();
        }
        if (numberofiteminexcess == 0)
        {
            item2.amount = 0;
            Destroy(item2.gameObject);
            item2 = null;
            return;
        }
        else
        {
            item2.amount = numberofiteminexcess;
            if (item.myText != null)
            {
                item2.UpdateTextAmount();
            }
        }
        if (item2.amount == 0)
        {
            Destroy(item2.gameObject);
            item2 = null;
        }
        
    }
    public void releaseItem()
    {
        item = null;
    }
    public void SwapTwoItem(Item item1, Item item2)
    {
        bool canswap = true;
        OvenInventoryItem itemC, itemC2;
        if (item1.parent.TryGetComponent<OvenInventoryItem>(out itemC))
        {
            if(itemC.typeSlot == OvenInventoryItem.TypeOvenSlot.Sortie)
            {
                canswap = false;
            }
        }
        if (item2.parent.TryGetComponent<OvenInventoryItem>(out itemC2))
        {
            if (itemC2.typeSlot == OvenInventoryItem.TypeOvenSlot.Sortie)
            {
                canswap = false;
            }
        }
        if(canswap)
        {
            Transform parent = item2.transform.parent;
            item2.transform.SetParent(item1.transform.parent);
            item1.transform.SetParent(parent);
            item1.parent = parent.gameObject;
            parent.gameObject.GetComponent<InventoryItem>().item = item1;
            item1.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }
    virtual public void DropItem(Item ItemDropped)
    {
        if (item == null)
        {
            ItemDropped.gameObject.transform.SetParent(Position);
            AddItemtoSlot(ItemDropped);
            item.parent = gameObject;
        }
        else
        {
            if (item.ItemData.id != ItemDropped.ItemData.id && canSwap)
            {
                SwapTwoItem(item, ItemDropped);
                if (GetComponent<OvenInventoryItem>() != null && !CanvasOven.instance.lastOven.iscooking)
                {
                    CanvasOven.instance.lastOven.StopCooking(false);
                }
            }
            else
            {
                AddtwoItem(item, ItemDropped);
                item.UpdateTextAmount();
                if(GetComponent<OvenInventoryItem>() != null && !CanvasOven.instance.lastOven.iscooking)
                {
                    CanvasOven.instance.lastOven.StopCooking(false);
                }
            }
        }        
    }
    public GameObject DetachItem()
    {
        if (item != null)
        {
            item.parent = null;
            GameObject gameObject = item.gameObject;
            item = null;
            gameObject.transform.SetParent(null);
            return gameObject;
        }
        return null;
    }

    public virtual void Event(PointerEventData eventData)
    {
        return;
    } 
}
