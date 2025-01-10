using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public InventoryMenu inventaire;
    public ActionBar ActionBar;
    public Sprite Background;
    public GameObject CanvasPickup;

    public static Inventory instance;


    private void Start()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Debug.LogWarning("Une autre instance de ListeItems a �t� trouv�e et d�truite.");
        }
        
        inventaire.StartInventaire();
        ActionBar.StartInventaire();
        
        GameObject gameObject = Instantiate(ListAllItems.instance.listeallItems[3].prefabIcon);
        gameObject.GetComponent<Item>().amount = 3;
        AddtoInventory(gameObject);
        GameObject gameObject2 = Instantiate(ListAllItems.instance.listeallItems[2].prefabIcon);
        gameObject2.GetComponent<Item>().amount = 3;
        AddtoInventory(gameObject2);
        ActionBar.Reload3DObjects();
        ToogleInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponentInChildren<ActionBar>().Reload3DObjects();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToogleInventory();
        }
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape))
        {
            inventaire.gameObject.SetActive(false);
        }
    }
    public void ToogleInventory()
    {
        inventaire.gameObject.SetActive(!inventaire.gameObject.activeInHierarchy);
        ActionBar.ToogleCanDragitem();
        if (inventaire.gameObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void AddtoInventory(GameObject item)
    {
        bool wasaddedfully = ActionBar.AddIconIventaire(item);
        if (!wasaddedfully)
        {
            wasaddedfully = inventaire.AddIconIventaire(item);
        }
        else
        {
            ActionBar.Reload3DObjects();
        }
        if (!wasaddedfully)
        {
            while (!wasaddedfully)
            {
                GameObject ObjectDroped = Instantiate(ListAllItems.instance.listeallItems[item.GetComponent<Item>().ItemData.id].prefab3D, new Vector3(0, 10, 0), Quaternion.identity);
                GameObject canvas = new();
                GameObject CanvasForPickup = Instantiate(CanvasPickup);
                ObjectDroped.layer = 7;
                ObjectDroped.AddComponent<ItemPickup>();
                ObjectDroped.GetComponent<ItemPickup>()._prompt = "Ramasser";
                ObjectDroped.AddComponent<InteractionPromptUI>();
                ObjectDroped.GetComponent<InteractionPromptUI>()._uiPanel = CanvasForPickup;
                ObjectDroped.GetComponent<InteractionPromptUI>()._promptText = CanvasForPickup.GetComponentInChildren<TextMeshProUGUI>();
                CanvasForPickup.AddComponent<CanvasAboveObject>();
                CanvasForPickup.transform.SetParent(ObjectDroped.transform);
                CanvasForPickup.transform.position = new Vector3(0, 0.1f, 0);
                canvas.name = "Canvasimage";
                canvas.transform.SetParent(ObjectDroped.transform);
                canvas.AddComponent<Canvas>();
                if (item.GetComponent<Item>().amount <= item.GetComponent<Item>().ItemData.amountStockableMax)
                {
                    ObjectDroped.GetComponent<Item3d>().IconItem = item;
                    //ObjectDroped.GetComponent<Item3d>().IconItem.SetActive(false);
                    wasaddedfully=true;
                }
                else {
                    GameObject ItemCopy = Instantiate(item);
                    ItemCopy.GetComponent<Item>().amount = item.GetComponent<Item>().ItemData.amountStockableMax;
                    item.GetComponent<Item>().amount -= item.GetComponent<Item>().ItemData.amountStockableMax;
                    ObjectDroped.GetComponent<Item3d>().IconItem = ItemCopy;
                    //ItemCopy.SetActive(false);
                }
                ObjectDroped.GetComponent<Item3d>().IconItem.transform.SetParent(canvas.transform);
                ObjectDroped.GetComponent<Item3d>().IconItem.GetComponent<Image>().sprite = ObjectDroped.GetComponent<Item3d>().IconItem.GetComponent<Item>().ItemData.iconImage;
                ObjectDroped.AddComponent<MeshCollider>();
                ObjectDroped.GetComponent<MeshCollider>().convex = true;
                ObjectDroped.AddComponent<Rigidbody>();
                canvas.SetActive(false);
            }
        }
    }
    public void AddtoInventorybyItem3d(GameObject item3d)
    {
        GameObject icon = item3d.GetComponent<Item3d>().IconItem;
        icon.transform.SetParent(null);
        item3d.GetComponent<Item3d>().IconItem = null;
        AddtoInventory(icon);
    }

    public bool HasItem(ItemDataAndAmount item)
    {
        if (item.requiredItem.id  < 0 || item.requiredItem.id > ListAllItems.instance.listeallItems.Count)
        {
            Debug.LogError("L'indice pass� en param�tre est inferieur à 0 ou superieur à la longeur du tableau.");
            return false;
        }
        int countItem = 0;
        foreach (var invItem in inventaire.ListeObjets) 
        {
            if (invItem.item != null && invItem.item.ItemData.id == item.requiredItem.id)
            {
                countItem += invItem.item.amount;
                if (countItem >= item.amount)
                {
                    return true;
                }
            }
        }

        foreach (var actionBarItem in ActionBar.ListeObjets)
        {
            if (actionBarItem.item != null && actionBarItem.item.ItemData.id == item.requiredItem.id)
            {
                countItem += actionBarItem.item.amount;
                if (countItem >= item.amount)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public void DeleteItems(ItemDataAndAmount[] ids)
    {
        //if (ids.Length != amounts.Length)
        //{
        //    Debug.LogWarning("Les ids et les montants n'ont pas la même longueur de tableau");
        //    return;
        //}
        for (int i = 0; i < ids.Length; i++)
        {
            if (!ActionBar.DeleteItem(ids[i].requiredItem.id, ids[i].amount))
            {
                inventaire.DeleteItem(ids[i].requiredItem.id, ids[i].amount);
            }
        }
    }
}
