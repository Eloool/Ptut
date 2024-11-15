using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventaire : MonoBehaviour
{
    private List<InventoryItem> ListeObjets;
    private List<InventoryItem> ListeArmure;

    private void Start()
    {
        ListeObjets = GetComponentsInChildren<InventoryItem>().ToList();
        ListeObjets.RemoveRange(ListeObjets.Count-4, 4);
        ListeArmure = GetComponentsInChildren<InventoryItem>().ToList();
        ListeArmure.RemoveRange(0, ListeObjets.Count);
        AddIconIventaire(2, 7);
        AddIconIventaire(2, 7);
        AddIconIventaire(0, 8);
        AddIconIventaire(0, 9);
        for (int i = 0; i < ListeObjets.Count; i++)
        {
            Debug.Log(ListeObjets[i].item.transform.parent);

        }
        //AddIconIventaire(2, 5);
    }
    public void AddIconIventaire(int id , int amount)
    {
        if (id < GetComponent<ListeItems>().listeItems.Count() - 1 && id >= 0)
        {
            int numbericons = 1 + amount / GetComponent<ListeItems>().listeItems[id].GetComponent<Item>().amountStockableMax;
            if (GetComponent<ListeItems>().listeItems[id].GetComponent<Item>().amountStockableMax == 1)
            {
                --numbericons;
            }
            GameObject[] newIcons = new GameObject[numbericons];
            int numberdone = 0;
            while (numberdone < numbericons)
            {
                newIcons[numberdone] = Instantiate(GetComponent<ListeItems>().listeItems[id]);
                GameObject icon = newIcons[numberdone];
                if (numberdone == numbericons - 1)
                {
                    icon.GetComponent<Item>().amount = amount;
                }
                else
                {
                    icon.GetComponent<Item>().amount = icon.GetComponent<Item>().amountStockableMax;
                }
                bool asadded = false;
                for (int i = 0; i < ListeObjets.Count; i++)
                {
                    if (ListeObjets[i].item == null)
                    {
                        //Debug.Log(icon.GetComponent<Item>().name);
                        //Debug.Log(icon.GetComponent<Item>().amount);
                        ListeObjets[i].AddItemtoSlot(icon.GetComponent<Item>());
                        icon.GetComponent<Item>().gameObject.transform.parent = ListeObjets[i].transform;
                        ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                        ListeObjets[i].item.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        icon.GetComponent<Item>().parent = ListeObjets[i].gameObject;
                        ListeObjets[i].item.gameObject.GetComponent<Image>().sprite = GetComponent<ListeItems>().listeItems[id].GetComponent<Item>().iconImage;
                        icon.GetComponent<Item>().CreateTextAmount();
                        asadded = true;
                        break;
                    }
                    else 
                    {
                        if (ListeObjets[i].item.GetComponent<Item>().id == icon.GetComponent<Item>().id &&
                            ListeObjets[i].item.GetComponent<Item>().amount< icon.GetComponent<Item>().amountStockableMax)
                        {
                            ListeObjets[i].AddtwoItem(ListeObjets[i].item, icon.GetComponent<Item>());
                            ListeObjets[i].item.UpdateTextAmount();

                        }
                    }
                }
                
                amount -= newIcons[numberdone].GetComponent<Item>().amountStockableMax;
                numberdone++;
            }
        }
        else
        {
            Debug.LogError("Mauvais id");
        }
    }
}
