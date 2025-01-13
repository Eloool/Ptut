using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : InteractableBase
{ 
    [SerializeField]
    private List<ItemDataAmountProbability> _probabilityDrop = new List<ItemDataAmountProbability>();

    private float PercentHealthLost = 0.0f;

    private void Awake()
    {
        gameObject.layer = 8;
        for (int i = 0; i < _probabilityDrop.Count; i++)
        {
            if (_probabilityDrop[i].amountEach10Percentage * 10 > _probabilityDrop[i].amountTotal)
            {
                Debug.LogError(gameObject + " a trop de amountEach10Percentage pour l'amountTotal à la place : " + i);
            }
        }
    }

    override public void GotHit(Item item)
    {
        HitObjectStat stat;
        if (item == null || !item.TryGetStat<HitObjectStat>(out stat))
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

        if (health > 0 && PercentHealthLost>=0.1f)
        {
            foreach (ItemDataAmountProbability probability in _probabilityDrop)
            {
                GameObject itemDropped = Instantiate(ListAllItems.instance.listeallItems[probability.Item.id].prefabIcon);
                while (PercentHealthLost >= 0.1f)
                {
                    itemDropped.GetComponent<Item>().amount += probability.amountEach10Percentage;
                    PercentHealthLost -= 0.1f;
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
    [Range(1, 10)]
    public int amountEach10Percentage;
    [Range(1, 100)]
    public int amountTotal;
}
