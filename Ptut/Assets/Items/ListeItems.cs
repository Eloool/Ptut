using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ListeItems : MonoBehaviour
{
    [System.Serializable]
    public class iconand3d
    {
        public GameObject Icon;
        public GameObject Objet3d;
    }
    private Inventaire inventaire;
    private ActionBar ActionBar;
    public List<iconand3d> listeallItems;
    public Sprite Background;
    public GameObject CanvasPickup;

    public static ListeItems instance; // Propriété statique

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Initialisation si instance n'existe pas
            Debug.Log("ListeItems.instance a été initialisé.");
        }
        else
        {
            Debug.LogWarning("Une autre instance de ListeItems a été trouvée et détruite.");
        }
        listeallItems.Sort(delegate (iconand3d x, iconand3d y)
        {
            return x.Icon.GetComponent<Item>().id.CompareTo(y.Icon.GetComponent<Item>().id);
        });
        inventaire = GetComponentInChildren<Inventaire>();
        inventaire.StartInventaire();
        ActionBar = GetComponentInChildren<ActionBar>();
        ActionBar.StartInventaire();
        int countingid = 0;
        for (int i = 0; i < listeallItems.Count; countingid++)
        {
            if (listeallItems[i].Icon.GetComponent<Item>().id != countingid)
            {
                Debug.LogError("Pas d'items avec l'id" + countingid);
            }
            else
            {
                i++;
            }
        }
        GameObject gameObject = Instantiate(listeallItems[4].Icon);
        gameObject.GetComponent<Item>().amount = 36;
        AddtoInventory(gameObject);
        ActionBar.Reload3DObjects();
    }
    private void Start()
    {
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
    }
    public void ToogleInventory()
    {
        inventaire.gameObject.SetActive(!inventaire.gameObject.activeInHierarchy);
        ActionBar.ToogleCanDragitem();
    }
    public void AddtoInventory(GameObject item)
    {
        bool wasaddedfully = ActionBar.AddIconIventaire(item);
        if (!wasaddedfully)
        {
            wasaddedfully = inventaire.AddIconIventaire(item);
        }
        if (!wasaddedfully)
        {
            while (!wasaddedfully)
            {
                GameObject ObjectDroped = Instantiate(listeallItems[item.GetComponent<Item>().id].Objet3d, new Vector3(0, 10, 0), Quaternion.identity);
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
                if (item.GetComponent<Item>().amount <= item.GetComponent<Item>().amountStockableMax)
                {
                    ObjectDroped.GetComponent<Item3d>().IconItem = item;
                    //ObjectDroped.GetComponent<Item3d>().IconItem.SetActive(false);
                    wasaddedfully=true;
                }
                else {
                    GameObject ItemCopy = Instantiate(item);
                    ItemCopy.GetComponent<Item>().amount = item.GetComponent<Item>().amountStockableMax;
                    item.GetComponent<Item>().amount -= item.GetComponent<Item>().amountStockableMax;
                    ObjectDroped.GetComponent<Item3d>().IconItem = ItemCopy;
                    //ItemCopy.SetActive(false);
                }
                ObjectDroped.GetComponent<Item3d>().IconItem.transform.SetParent(canvas.transform);
                ObjectDroped.GetComponent<Item3d>().IconItem.GetComponent<Image>().sprite = ObjectDroped.GetComponent<Item3d>().IconItem.GetComponent<Item>().iconImage;
                ObjectDroped.AddComponent<MeshCollider>();
                ObjectDroped.GetComponent<MeshCollider>().convex = true;
                ObjectDroped.AddComponent<Rigidbody>();
                canvas.SetActive(false);
            }
        }
    }
}
