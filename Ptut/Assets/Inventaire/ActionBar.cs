using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : InventoryBase
{
    public GameObject Cadre;
    public bool canscroll= true;
    private int SlotActuel = 0;
    private GameObject[] Objects3d;

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && canscroll)
        {
            if (ListeObjets[SlotActuel].item != null)
            {
                Objects3d[SlotActuel].SetActive(false);
            }
            SlotActuel -= (int)Input.mouseScrollDelta.y;
            if (SlotActuel > ListeObjets.Count - 1)
            {
                SlotActuel = ListeObjets.Count - 1;
            }
            else if (SlotActuel < 0)
            {
                SlotActuel = 0;
            }
            Cadre.transform.SetParent(ListeObjets[SlotActuel].transform);
            Cadre.transform.localPosition = new Vector3(0, 0, 0);
            if (ListeObjets[SlotActuel].item != null)
            {
                Objects3d[SlotActuel].SetActive(true);
            }
            Hand.instance.ChangeObject(Objects3d[SlotActuel]);
        }
    }

    public void Reload3DObjects()
    {
        for (int i = 0; i < Objects3d.Length; i++)
        {
            Destroy(Objects3d[i]);
        }
        Objects3d = new GameObject[ListeObjets.Count];
        for (int i = 0; i < ListeObjets.Count; i++)
        {
            if (ListeObjets[i].item != null)
            {
                Objects3d[i]=Instantiate(ListAllItems.instance.listeallItems[ListeObjets[i].item.ItemData.id].Objet3d);
                Objects3d[i].GetComponent<Item3d>().IconItem = ListeObjets[i].item.gameObject;
                Objects3d[i].SetActive(i==SlotActuel);
            }
        }
        Hand.instance.ChangeObject(Objects3d[SlotActuel]);
    }
    public override void ToogleCanDragitem()
    {
        base.ToogleCanDragitem();
        canscroll = !canscroll;
    }
    public override void StartInventaire()
    {
        ListeObjets = GetComponentsInChildren<InventoryItem>().ToList();
        foreach (InventoryItem item in ListeObjets)
        {
            item.gameObject.GetComponent<Image>().sprite = transform.parent.GetComponent<Inventory>().Background;
        }
        Objects3d = new GameObject[ListeObjets.Count]; 
    }
}
