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

    private RecipeData recipe;

    private int RecipeAmount;

    public void Configure(RecipeData recipe)
    {
        currentRecipe = recipe;
        //Item = currentRecipe.craftableItem.prefab;

        craftableItemImage.sprite = recipe.craftableItem.iconImage;

        RecipeAmount = recipe.amount;

        bool canCraft = true; 

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            int requiredItem = recipe.requiredItems[i].id; // Récupère le prefab GameObject pour cet item requis

            if (!Inventory.instance.HasItem(requiredItem))
            {
                Debug.Log("Pas de " + recipe.requiredItems[i].id);
                canCraft = false;
            }

            GameObject requiredItemGO = Instantiate(elementRequiredPrefab, elementsRequiredPrefab);
            requiredItemGO.transform.GetChild(0).GetComponent<Image>().sprite = recipe.requiredItems[i].iconImage;
        }

        craftButton.image.sprite = canCraft ? canBuildIcon : cantBuildIcon;
        craftButton.enabled = canCraft;
        Debug.Log("Etat de canCraft = " + canCraft);

        ResizeElementsRequiredParent();
    }

    private void ResizeElementsRequiredParent()
    {
        Canvas.ForceUpdateCanvases();
        elementsRequiredPrefab.GetComponent<ContentSizeFitter>().enabled = false;
        elementsRequiredPrefab.GetComponent<ContentSizeFitter>().enabled = true;
    }

    public void CraftItem()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("ListeItems.instance est null.");
            return;
        }

        GameObject prefab = currentRecipe.craftableItem.prefab;
        if (prefab == null)
        {
            Debug.LogError("Prefab du craftableItem est null.");
            return;
        }

        // Instanciation de l'objet à partir du prefab
        GameObject instance = Instantiate(prefab);
        instance.GetComponent<Item>().amount = RecipeAmount;
        Inventory.instance.AddtoInventory(instance); 
    }
}
