using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ListeItems : MonoBehaviour
{
    [System.Serializable]
    public class iconand3d
    {
        public GameObject Icon;      // probabilité d'avoir cet objet quand il faut ajouter un obstacle
        public GameObject Objet3d;
    }
    private Inventaire inventaire;
    private ActionBar ActionBar;
    public List<iconand3d> listeallItems;
    public Sprite Background;

    private void Awake()
    {
        listeallItems.Sort(delegate (iconand3d x, iconand3d y) {
            return x.Icon.GetComponent<Item>().id.CompareTo(y.Icon.GetComponent<Item>().id);
        });
        inventaire = GetComponentInChildren<Inventaire>();
        inventaire.StartInventaire();
        ActionBar = GetComponentInChildren<ActionBar>();
        ActionBar.StartInventaire();
        int countingid =0;
        for (int i = 0; i < listeallItems.Count; countingid++) {
            if (listeallItems[i].Icon.GetComponent<Item>().id != countingid)
            {
                Debug.LogError("Pas d'items avec l'id" + countingid);
            }
            else
            {
                i++;
            }
        }
        inventaire.AddIconIventaire(1, 2);
        
    }
    private void Start()
    {
        ToogleInventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponentInChildren<ActionBar>().Reload3DObjects();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToogleInventory();
        }
    }
    public void ToogleInventory()
    {
        inventaire.gameObject.SetActive(!inventaire.gameObject.activeInHierarchy);
        ActionBar.ToogleCanDragitem();
    }
    public void AddtoInventory(GameObject item)
    {

    }
}
