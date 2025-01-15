using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAddingInventory : MonoBehaviour
{
    [SerializeField]
    private Transform CanvasParent;

    [SerializeField]
    private GameObject canvasItemPrefab;

   public void AddCanvasItem(Item item, int amount)
    {
        GameObject CanvasItem = Instantiate(canvasItemPrefab, CanvasParent);
        CanvasItem.GetComponentInChildren<TMP_Text>().text = item.ItemData.ItemName + " * " + amount.ToString();
        CanvasItem.GetComponentInChildren<Image>().sprite = item.ItemData.iconImage;
        StartCoroutine(DeleteItem(CanvasItem));
    } 

    IEnumerator DeleteItem(GameObject canvasItem)
    {
        yield return new WaitForSeconds(10f);
        Destroy(canvasItem);
    }
}
