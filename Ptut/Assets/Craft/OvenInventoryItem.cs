using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OvenInventoryItem : InventoryItem
{
    public enum TypeOvenSlot
    {
        Combustible,
        Cooking,
        Sortie
    }
    public TypeOvenSlot typeSlot;

    public override void DropItem(Item ItemDropped)
    {
        if (typeSlot != TypeOvenSlot.Sortie && ItemDropped.slot!=2)
        {
            base.DropItem(ItemDropped);
            switch (typeSlot)
            {
                case TypeOvenSlot.Combustible:
                    CanvasOven.instance.lastOven.SetCombustible(item);
                    break;
                case TypeOvenSlot.Cooking:
                    CanvasOven.instance.lastOven.SetBruler(item);
                    break;
                default:
                    break;
            }
            if (GetComponent<OvenInventoryItem>() != null && ItemDropped.gameObject.GetComponentInParent<OvenInventoryItem>() != null && ItemDropped != item)
            {
                if (typeSlot == TypeOvenSlot.Combustible)
                {
                    CanvasOven.instance.lastOven.SetBruler(ItemDropped);
                }
                else if (typeSlot == TypeOvenSlot.Cooking)
                {
                    CanvasOven.instance.lastOven.SetCombustible(ItemDropped);
                }
            }
        }
        
    }
    public void SetItem(GameObject Item)
    {
        if (Item != null)
        {
            Item ItemSet = Item.GetComponent<Item>();
            Item.transform.SetParent(this.transform);
            ItemSet.parent = this.gameObject;
            item = ItemSet;
        }
    }
    public void DropItemDirect(Item Item)
    {
        base.DropItem(Item);
    }
}
