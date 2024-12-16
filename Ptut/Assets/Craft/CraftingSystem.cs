using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField]
    private RecipeData[] availableRecipes;

    [SerializeField]
    private GameObject recipeUiPrefab;

    [SerializeField]
    private Transform recipesParent;

    [SerializeField]
    private GameObject craftingTable; 

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplayedRecipes();
        craftingTable.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCraftTable(); 
        }
    }

    private void UpdateDisplayedRecipes()
    {
        for (int i = 0; i < availableRecipes.Length; i++)
        {
            GameObject recipe = Instantiate(recipeUiPrefab, recipesParent);
            recipe.GetComponent<Recipe>().Configure(availableRecipes[i]);
        }
    }

    public void ToggleCraftTable()
    {
        craftingTable.SetActive(!craftingTable.activeSelf);
    }

}
