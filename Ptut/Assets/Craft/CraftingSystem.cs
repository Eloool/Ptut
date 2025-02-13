using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSystem : ToogleCanvas
{ 
    [SerializeField]
    private RecipeData[] availableRecipes;

    private List<GameObject> recipeUis =new List<GameObject>();

    [SerializeField]
    private GameObject recipeUiPrefab;

    [SerializeField]
    private Transform recipesParent;

    [SerializeField]
    private GameObject craftingTable; 

    public static CraftingSystem instance;


    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        craftingTable.SetActive(false);
        Debug.Log("recette disponible " + availableRecipes.Length);
        StartCraftingCanvas();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!craftingTable.activeInHierarchy)
            {
                CanvasController.instance.ShowCanvas(this);
            }
            else
            {
                CanvasController.instance.HideAllCanvases();
            }
        }
    }

    public void UpdateDisplayedRecipes()
    {
        EventSystem.current.SetSelectedGameObject(null);

        Inventory.instance.ReloadItems();
        foreach(GameObject recipe in recipeUis)
        {
            recipe.GetComponent<Recipe>().CheckIfCanCraft();
        }
    }

    public void ToggleCraftTable(bool active)
    {
        craftingTable.SetActive(active);
    }

    public override void SetActiveCanvas(bool active)
    {
        if (active)
        {
            ToggleCraftTable(true);
            Inventory.instance.ActionBar.SetCanScroll(false);
            UpdateDisplayedRecipes();
        }
        else
        {
            ToggleCraftTable(false);
        }
    }

    private void StartCraftingCanvas()
    {
        EventSystem.current.SetSelectedGameObject(null);

        foreach (Transform child in recipesParent)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }

        Inventory.instance.ReloadItems();
        for (int i = 0; i < availableRecipes.Length; i++)
        {
            GameObject recipe = Instantiate(recipeUiPrefab, recipesParent);
            recipe.GetComponent<Recipe>().Configure(availableRecipes[i]);
            recipeUis.Add(recipe);
        }
    }
}
