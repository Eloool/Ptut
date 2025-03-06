using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListAllItems : MonoBehaviour
{
    public List<ItemData> listeallItems;
    public static ListAllItems instance;
    public GameObject CanvasPickup;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Initialisation si instance n'existe pas
            Debug.Log("ListAllItems.instance a �t� initialis�.");
        }
        else
        {
            Debug.LogWarning("Une autre instance de ListeItems a �t� trouv�e et d�truite.");
        }
        foreach (ItemData item in listeallItems)
        {
            if(item.prefabIcon.GetComponent<Item>().ItemData == null)
            {
                Debug.LogWarning("Pas d'ItemData pour " + item.prefabIcon.GetComponent<Item>());
            }
        }
        listeallItems.Sort(delegate (ItemData x, ItemData y)
        {
            return x.prefabIcon.GetComponent<Item>().ItemData.id.CompareTo(y.prefabIcon.GetComponent<Item>().ItemData.id);
        });


        int countingid = 0;
        for (int i = 0; i < listeallItems.Count; countingid++)
        {
            if (listeallItems[i].prefabIcon.GetComponent<Item>().ItemData.id != countingid)
            {
                Debug.LogError("Pas d'items avec l'id" + countingid);
            }
            else
            {
                i++;
            }
        }
    }
    public static GameObject CreateIcon(int id, int amount)
    {
        GameObject icon = Instantiate(instance.listeallItems[id].prefabIcon,new Vector3(0,0,0),Quaternion.identity);
        icon.GetComponent<Item>().amount = amount;
        return icon;
    }

    public static bool Create3DItem(GameObject item, Transform drop)
    {
        bool wasaddedfully = false;
        GameObject gameObject = Instantiate(instance.listeallItems[item.GetComponent<Item>().ItemData.id].prefab3D, drop.position, Quaternion.identity);
        GameObject canvas = new();
        GameObject CanvasForPickup = Instantiate(instance.CanvasPickup);
        gameObject.layer = 7;
        gameObject.AddComponent<ItemPickup>();
        gameObject.GetComponent<ItemPickup>()._prompt = "Pick up (E)";
        gameObject.AddComponent<InteractionPromptUI>();
        gameObject.GetComponent<InteractionPromptUI>()._uiPanel = CanvasForPickup;
        gameObject.GetComponent<InteractionPromptUI>()._promptText = CanvasForPickup.GetComponentInChildren<TextMeshProUGUI>();
        CanvasForPickup.AddComponent<CanvasAboveObject>();
        CanvasForPickup.transform.SetParent(gameObject.transform);
        CanvasForPickup.transform.position = new Vector3(0, 0.1f, 0);
        canvas.name = "Canvasimage";
        canvas.transform.SetParent(gameObject.transform);
        canvas.AddComponent<Canvas>();
        if (item.GetComponent<Item>().amount <= item.GetComponent<Item>().ItemData.amountStockableMax)
        {
            gameObject.GetComponent<Item3d>().IconItem = item;
            //ObjectDroped.GetComponent<Item3d>().IconItem.SetActive(false);
            wasaddedfully = true;
        }
        else
        {
            GameObject ItemCopy = Instantiate(item);
            ItemCopy.GetComponent<Item>().amount = item.GetComponent<Item>().ItemData.amountStockableMax;
            item.GetComponent<Item>().amount -= item.GetComponent<Item>().ItemData.amountStockableMax;
            gameObject.GetComponent<Item3d>().IconItem = ItemCopy;
            //ItemCopy.SetActive(false);
        }
        gameObject.GetComponent<Item3d>().IconItem.transform.SetParent(canvas.transform);
        gameObject.GetComponent<Item3d>().IconItem.GetComponent<Image>().sprite = gameObject.GetComponent<Item3d>().IconItem.GetComponent<Item>().ItemData.iconImage;
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        canvas.SetActive(false);
        return wasaddedfully;
    }
}
