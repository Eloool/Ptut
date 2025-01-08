using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public enum TypeItem
    {
        Casque,
        Torse,
        Pantalon,
        Bottes,
        Epee,
        Pioche,
        Hache,
        Arc,
        Autre
    }
    public TypeItem TypeOfItem;
    public int id;
    public string ItemName;
    public string ItemDescription;
    public int amountStockableMax;
    public GameObject prefabIcon;
    public GameObject prefab3D;
    public Sprite iconImage;
}
