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

    private List<GameObject> items = new List<GameObject>();

    public void AddCanvasItem(Item item, int amount)
    {
        GameObject existingItem = null;

        foreach (GameObject child in items)
        {
            TMP_Text textComponent = child.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                string[] split = textComponent.text.Split('*');
                if (split[0].Trim() == item.ItemData.ItemName)
                {
                    existingItem = child;
                    break;
                }
            }
        }

        if (existingItem != null)
        {
            TMP_Text textComponent = existingItem.GetComponentInChildren<TMP_Text>();
            string[] split = textComponent.text.Split('*');
            int currentAmount = int.Parse(split[1].Trim());
            textComponent.text = item.ItemData.ItemName + " * " + (currentAmount + amount).ToString();

            StopCoroutine(DeleteItem(existingItem));
            StartCoroutine(DeleteItem(existingItem));
        }
        else
        {
            GameObject canvasItem = Instantiate(canvasItemPrefab, CanvasParent);
            canvasItem.GetComponentInChildren<TMP_Text>().text = item.ItemData.ItemName + " * " + amount;
            canvasItem.GetComponentInChildren<Image>().sprite = item.ItemData.iconImage;
            items.Add(canvasItem);
            StartCoroutine(DeleteItem(canvasItem));
        }
    }

    IEnumerator DeleteItem(GameObject canvasItem)
    {
        yield return new WaitForSeconds(5f);
        if (canvasItem != null)
        {
            items.Remove(canvasItem);
            Destroy(canvasItem);
        }
    }
}
