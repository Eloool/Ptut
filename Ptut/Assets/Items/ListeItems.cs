using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeItems : MonoBehaviour
{
    public GameObject[] listeItems;
    public GameObject[] listeItems3D;
    public Sprite Background;

    private void Start()
    {
        for (int i = 0; i < listeItems.Length; i++)
        {
            listeItems[i].GetComponent<Item>().id= i;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponentInChildren<ActionBar>().Reload3DObjects();
        }
    }
    public void HideInventory()
    {
        GetComponentInChildren<Inventaire>().gameObject.SetActive(false);
    }
    public void ShowInventory()
    {
        GetComponentInChildren<Inventaire>().gameObject.SetActive(true);
    }
}
