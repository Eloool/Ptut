using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public string ItemName;
    public string description;
    public int amount;
    public int amountStockableMax;
    public Sprite iconImage;

    private RectTransform rectTransform;
    private Canvas canvas;
    public Vector3 oldposition;

    private void Awake()
    {
        GetComponent<Image>().sprite = iconImage;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        oldposition = rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
    }
    #region IDragHandler implementation
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;  // Déplace l'image en fonction de la position de la souris
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = oldposition;
        GetComponent<Image>().raycastTarget = true;
    }
    #endregion
}
