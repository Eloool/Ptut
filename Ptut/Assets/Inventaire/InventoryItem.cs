using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,IDropHandler
{
    public Item item;

    private RectTransform Position;
    private RectTransform RectParent;

    private void Start()
    {
        Position = GetComponent<RectTransform>();
        RectParent = transform.parent.GetComponent<RectTransform>();
        //item = null;
    }
    public void AddItemtoSlot(Item iteme)
    {
        item = iteme;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (item == null || item == eventData.pointerDrag.GetComponent<Item>())
        {
            eventData.pointerDrag.gameObject.transform.parent = Position;
            AddItemtoSlot(eventData.pointerDrag.GetComponent<Item>());
            item.parent = gameObject;
        }
        else
        {
            if(item.id == eventData.pointerDrag.GetComponent<Item>().id)
            {
                AddtwoItem(item, eventData.pointerDrag.GetComponent<Item>());
                //int numberofiteminexcess = 0;
                //item.amount += eventData.pointerDrag.GetComponent<Item>().amount;
                //if(item.amount > item.amountStockableMax)
                //{
                //   numberofiteminexcess =item.amount- item.amountStockableMax ;
                //    item.amount-= numberofiteminexcess;
                //    item.ResetTextAmount();
                //}
                //if (numberofiteminexcess == 0)
                //{
                //    Destroy(eventData.pointerDrag.gameObject);
                //}
                //else
                //{
                //    eventData.pointerDrag.GetComponent<Item>().amount = numberofiteminexcess;
                //    eventData.pointerDrag.GetComponent<Item>().ResetTextAmount();
                //}
            }
        }
    }
    public void AddtwoItem(Item item1, Item item2)
    {
        int numberofiteminexcess = 0;
        item1.amount += item2.amount;
        if (item1.amount > item1.amountStockableMax)
        {
            numberofiteminexcess = item1.amount - item1.amountStockableMax;
            item1.amount -= numberofiteminexcess;
            item1.UpdateTextAmount();
        }
        if (numberofiteminexcess == 0)
        {
            Destroy(item2.gameObject);
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
        }
        
    }
    public void releaseItem()
    {
        item = null;
    }
}
