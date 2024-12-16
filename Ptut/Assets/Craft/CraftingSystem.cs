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
        craftingTable.SetActive(false);
        Debug.Log("recette disponible " + availableRecipes.Length);
    }

    void Update()
    {
        UpdateDisplayedRecipes();

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCraftTable(); 
        }
    }

    private void UpdateDisplayedRecipes()
    {

        foreach(Transform child in recipesParent)
        {
            Destroy(child.gameObject);
        }

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
