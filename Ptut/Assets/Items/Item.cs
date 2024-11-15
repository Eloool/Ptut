using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int id;
    public string ItemName;
    public string description;
    public int amount;
    public int amountStockableMax;
    public Sprite iconImage;

    private RectTransform rectTransform;
    private Canvas canvas;
    private GameObject textObject;
    public GameObject parent;
    public TMP_Text myText;

    private void Awake()
    {
        //GetComponent<Image>().sprite = iconImage;
        rectTransform = GetComponent<RectTransform>();
        //zCreateTextAmount();
        //parent = null;
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
        GetComponent<Image>().raycastTarget = false;
        if (parent != null)
        {
            parent.transform.SetAsLastSibling();
            parent.transform.parent.transform.SetAsLastSibling();
            parent.GetComponent<InventoryItem>().releaseItem();
            UpdateTextAmount();
        }
        transform.SetAsLastSibling();

    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;  // Déplace l'image en fonction de la position de la souris
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent != parent)
        {
            rectTransform.anchoredPosition = new Vector2(0, 0);
            transform.parent.GetComponent<InventoryItem>().item = this;
            GetComponent<Image>().raycastTarget = true;
            parent = transform.parent.gameObject;
        }

    }
    #endregion
    public void CreateTextAmount()
    {
            canvas = GetComponentInParent<Canvas>();
            //oldposition = rectTransform.localPosition;
            textObject = new("Nombre Item");
            textObject.transform.SetParent(this.transform);
            // le texte
            myText = textObject.AddComponent<TextMeshProUGUI>();
            myText.text = amount.ToString();
            myText.rectTransform.localScale = new Vector3(1, 1, 1);
            myText.rectTransform.sizeDelta = new Vector2(125, 125);
            myText.rectTransform.localPosition = new Vector3(0, (float)(-myText.rectTransform.sizeDelta.x / 1.5), 0);
       
    }
}
