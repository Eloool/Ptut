using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCraftTable();
            UpdateDisplayedRecipes();
        }
    }

    public void UpdateDisplayedRecipes()
    {
        EventSystem.current.SetSelectedGameObject(null);

        foreach (Transform child in recipesParent)
        {
            child.gameObject.SetActive(false); 
            Destroy(child.gameObject, 0.1f);   
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
        //ThirdPersonController.instance.enabled = !ThirdPersonController.instance.enabled;
        if (craftingTable.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
