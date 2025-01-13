using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item : Stats, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public ItemData ItemData;
    public int amount;
    public bool candragitem =false;
    private RectTransform rectTransform;
    private Canvas canvas;
    private GameObject textObject;
    public GameObject parent;
    public TMP_Text myText;
    public int slot = -1;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(110, 110);
    }

    public void UpdateTextAmount()
    {
        if (myText != null)
        {
            myText.text = amount.ToString();
        }
    }
    #region IDragHandler implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (candragitem)
        {
            GetComponent<Image>().raycastTarget = false;
            if (parent != null)
            {
                parent.transform.SetAsLastSibling();
                parent.transform.parent.transform.SetAsLastSibling();
                parent.transform.parent.transform.parent.transform.SetAsLastSibling();
                parent.GetComponent<InventoryItem>().releaseItem();
                UpdateTextAmount();
            }
            transform.SetAsLastSibling();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (candragitem)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;  // Déplace l'image en fonction de la position de la souris
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (candragitem)
        {
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            transform.parent.GetComponent<InventoryItem>().item = this;
            if(!CanvasOven.instance.lastOven.iscooking)
                CanvasOven.instance.lastOven.StopCooking(false);
            GetComponent<Image>().raycastTarget = true;
            parent = transform.parent.gameObject;
        }
    }
    #endregion
    public void CreateTextAmount()
    {
            canvas = Inventory.instance.GetComponent<Canvas>();
            textObject = new("Nombre Item");
            textObject.transform.SetParent(this.transform);
            
            // le texte
            myText = textObject.AddComponent<TextMeshProUGUI>();
            myText.text = amount.ToString();
            myText.rectTransform.localScale = new Vector3(1, 1, 1);
            myText.rectTransform.sizeDelta = new Vector2(60,20);
            myText.rectTransform.localPosition = new Vector3(-30, (float)(-myText.rectTransform.sizeDelta.x / 2), 0);
    }

    public void MinusOne()
    {
        amount--;
        if( amount == 0)
        {
            parent.GetComponent<InventoryItem>().item = null;
            Destroy(gameObject);
        }
        else
        {
            UpdateTextAmount();
        }
    }
}
