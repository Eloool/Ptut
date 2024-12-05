using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private RecipeData currentRecipe;

    [SerializeField]
    private Image craftableItemImage;

    [SerializeField]
    private GameObject elementRequiredPrefab;

    [SerializeField]
    private Transform elementsRequiredPrefab;

    [SerializeField]
    private Button craftButton;

    [SerializeField]
    private Sprite canBuildIcon;

    [SerializeField]
    private Sprite cantBuildIcon;


    public void Configure(RecipeData recipe)
    {
        currentRecipe = recipe;

        craftableItemImage.sprite = recipe.craftableItem.visual;

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            GameObject requiredItem = Instantiate(elementRequiredPrefab, elementsRequiredPrefab);
            requiredItem.transform.GetChild(0).GetComponent<Image>().sprite = recipe.requiredItems[i].visual;
        }

        ResizeElementsRequiredParent();

    }

    private void ResizeElementsRequiredParent()
    {
        Canvas.ForceUpdateCanvases();
        elementsRequiredPrefab.GetComponent<ContentSizeFitter>().enabled = false;
        elementsRequiredPrefab.GetComponent<ContentSizeFitter>().enabled = true;
    }

    private void CraftItem()
    {
        
    }
}
