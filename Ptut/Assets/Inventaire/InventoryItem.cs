using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,IDropHandler
{
    // Nombre d'items dans le slot
    [SerializeField] private int amountItem;

    // Image de l'item affiché
    [SerializeField] private Image itemImg;

    // Texte d'affichage du nombre d'item
    [SerializeField] private TextMeshProUGUI itemNumberTxt;

    // Type d'item
    [SerializeField] private Item item;

     private RectTransform Position;
    private RectTransform RectParent;

    private void Start()
    {
        Position = GetComponent<RectTransform>();
        RectParent = transform.parent.GetComponent<RectTransform>();
    }
    public void AddItemtoSlot(Item iteme)
    {
        item = iteme;
        amountItem = iteme.amount;
        //itemNumberTxt = new TextMeshProUGUI();
        //itemNumberTxt.text = amountItem.ToString();
    }

    public void OnDrop(PointerEventData eventData)
    {

        eventData.pointerDrag.GetComponent<Item>().oldposition = (Vector3)Position.anchoredPosition + (Vector3)RectParent.anchoredPosition ;
        AddItemtoSlot(eventData.pointerDrag.GetComponent<Item>());
    }
}
