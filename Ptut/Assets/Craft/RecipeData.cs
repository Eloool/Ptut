using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipes/New Recipe")]
public class RecipeData : ScriptableObject
{
    public ItemDataAndAmount craftableItem; 
    public ItemDataAndAmount[] requiredItems; 
}

[System.Serializable]
public class ItemDataAndAmount
{
    public ItemData requiredItem;
    public int amount; 
}
