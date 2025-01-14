using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSpot : MonoBehaviour ,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject itemdrag = eventData.pointerDrag.GetComponent<Item>().gameObject;
        GameObject ObjectDroped = Instantiate(ListAllItems.instance.listeallItems[itemdrag.GetComponent<Item>().ItemData.id].prefab3D, Inventory.instance.DropPoint.transform.position, Quaternion.identity);
        ObjectDroped.GetComponent<Item3d>().IconItem = itemdrag;
        itemdrag.GetComponent<Item>().parent.GetComponent<InventoryItem>().item = null;
        itemdrag.GetComponent<Item>().parent = null;
        GameObject canvas = new()
        {
            name = "canvasImage"
        };
        canvas.transform.parent = ObjectDroped.transform;
        canvas.AddComponent<Canvas>();
        ObjectDroped.AddComponent<BoxCollider>();
        ObjectDroped.AddComponent<Rigidbody>();
        ObjectDroped.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        itemdrag.transform.SetParent(canvas.transform);
        canvas.SetActive(false);
    }
}
