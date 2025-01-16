using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : InteractableBase
{ 
    public ItemData.TypeItem TypeItem;
    [SerializeField]
    private List<ItemDataAmountProbability> _probabilityDrop = new List<ItemDataAmountProbability>();

    private float PercentHealthLost = 0.0f;

    private void Awake()
    {
        gameObject.layer = 8;
        for (int i = 0; i < _probabilityDrop.Count; i++)
        {
            if (_probabilityDrop[i].amountEach25Percentage * 4 > _probabilityDrop[i].amountTotal)
            {
                Debug.LogError(gameObject + " a trop de amountEach10Percentage pour l'amountTotal à la place : " + i);
            }
        }
    }

    override public void GotHit(Item item)
    {
        HitObjectStat stat;
        if (item == null || (!item.TryGetStat<HitObjectStat>(out stat) && item.ItemData.TypeOfItem != TypeItem))
        {
            int HealthLost = 1;
            health -= HealthLost;
            PercentHealthLost += (float)HealthLost / (float)GetStat<HealthStat>().health;
        }
        else
        {
            int HealthLost = stat.hitObjectPower;
            health -= HealthLost;
            PercentHealthLost += (float)HealthLost / (float)GetStat<HealthStat>().health;
        }

        if (health > 0 && PercentHealthLost>=0.25f)
        {
            foreach (ItemDataAmountProbability probability in _probabilityDrop)
            {
                GameObject itemDropped = Instantiate(ListAllItems.instance.listeallItems[probability.Item.id].prefabIcon);
                while (PercentHealthLost >= 0.25f)
                {
                    itemDropped.GetComponent<Item>().amount += probability.amountEach25Percentage;
                    PercentHealthLost -= 0.25f;
                }
                probability.amountTotal -= itemDropped.GetComponent<Item>().amount;
                Inventory.instance.AddtoInventory(itemDropped);
            }
        }
        if(health <=0)
        {
            foreach (ItemDataAmountProbability probability in _probabilityDrop)
            {
                GameObject itemDropped = Instantiate(ListAllItems.instance.listeallItems[probability.Item.id].prefabIcon);
                itemDropped.GetComponent<Item>().amount = probability.amountTotal;
                Inventory.instance.AddtoInventory(itemDropped);
            }
            Destroy(gameObject);
        }
    }
}


[System.Serializable]
public class ItemDataAmountProbability
{
    public ItemData Item;
    [Range(0, 10)]
    public int amountEach25Percentage;
    [Range(1, 100)]
    public int amountTotal;
}
