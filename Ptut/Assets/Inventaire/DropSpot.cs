using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSpot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Destroy(eventData.pointerDrag.GetComponent<Item>().textObject);
        eventData.pointerDrag.GetComponent<Item>().myText = null;
        eventData.pointerDrag.GetComponent<Item>().textObject = null;
        ListAllItems.Create3DItem(eventData.pointerDrag.GetComponent<Item>().gameObject, Inventory.instance.DropPoint.transform);
    }
}
