using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        
        craftableItemImage.sprite = recipe.craftableItem.requiredItem.iconImage;
        CreateTextAmount(craftableItemImage.transform.parent.gameObject, recipe.craftableItem.amount);

        RecipeAmount = recipe.craftableItem.amount;

        bool canCraft = true;
        Debug.Log(recipe.craftableItem.requiredItem);
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            ItemDataAndAmount requiredItem = recipe.requiredItems[i]; // R�cup�re le prefab GameObject pour cet item requis

            if (!Inventory.instance.HasItem(requiredItem))
            {
                Debug.Log("Pas de " + recipe.requiredItems[i].requiredItem.id);
                canCraft = false;
            }
            GameObject requiredItemGO = Instantiate(elementRequiredPrefab, elementsRequiredPrefab);
            requiredItemGO.transform.GetChild(0).GetComponent<Image>().sprite = currentRecipe.requiredItems[i].requiredItem.iconImage;
            CreateTextAmount(requiredItemGO, recipe.requiredItems[i].amount);
            
        }
        Debug.Log(canCraft);
        craftButton.image.sprite = canCraft ? canBuildIcon : cantBuildIcon;
        craftButton.enabled = canCraft;
        //Debug.Log("Etat de canCraft = " + canCraft +" Recette : "+recipe);

        ResizeElementsRequiredParent();
    }

    private void ResizeElementsRequiredParent()
    {
        Canvas.ForceUpdateCanvases();
        elementsRequiredPrefab.GetComponent<ContentSizeFitter>().enabled = false;
        elementsRequiredPrefab.GetComponent<ContentSizeFitter>().enabled = true;
        Canvas.ForceUpdateCanvases();
    }

    public void CraftItem()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("ListeItems.instance est null.");
            return;
        }

        GameObject prefab = currentRecipe.craftableItem.requiredItem.prefabIcon;
        if (prefab == null)
        {
            Debug.LogError("Prefab du craftableItem est null.");
            return;
        }

        
        // Instanciation de l'objet � partir du prefab
        GameObject instance = Instantiate(prefab);
        instance.GetComponent<Item>().amount = RecipeAmount;

        Inventory.instance.DeleteItems(currentRecipe.requiredItems);
        Inventory.instance.AddtoInventory(instance);

        CraftingSystem.instance.UpdateDisplayedRecipes();
        Debug.Log("Hascraft");
    }
    public void CreateTextAmount(GameObject sprite , int amount)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        GameObject textObject = new("Nombre Item");
        textObject.transform.SetParent(sprite.transform.GetChild(0));

        // le texte
        TextMeshProUGUI myText = textObject.AddComponent<TextMeshProUGUI>();
        myText.text = amount.ToString();
        myText.rectTransform.localScale = new Vector3(0.7f,0.7f,0.7f);
        myText.rectTransform.sizeDelta = new Vector2(50, 20);
        myText.rectTransform.localPosition = new Vector3(-25, (float)(-myText.rectTransform.sizeDelta.x / 2), 0);
    }
}


