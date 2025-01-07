using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{
    [SerializeReference]
    private List<StatScriptableObject> _ObjectStats = new List<StatScriptableObject>();

    [SerializeField]
    private List<ItemDataAmountProbability> _probabilityDrop = new List<ItemDataAmountProbability>();


    private int health;

    private float PercentHealthLost = 0.0f;

    private void Awake()
    {
        gameObject.layer = 8;
        health = GetObjectStat<HealthStat>().health;
        for (int i = 0; i < _probabilityDrop.Count; i++)
        {
            if (_probabilityDrop[i].amountEach10Percentage * 10 > _probabilityDrop[i].amountTotal)
            {
                Debug.LogError(gameObject + " a trop de amountEach10Percentage pour l'amountTotal à la place : " + i);
            }
        }
    }

    public void GotHit(Item item)
    {
        HitObjectStat stat;
        if (item == null || !item.TryGetItemStat<HitObjectStat>(out stat))
        {
            int HealthLost = 1;
            health -= HealthLost;
            PercentHealthLost += (float)HealthLost / (float)GetObjectStat<HealthStat>().health;
        }
        else
        {
            int HealthLost = stat.hitObjectPower;
            health -= HealthLost;
            PercentHealthLost += (float)HealthLost / (float)GetObjectStat<HealthStat>().health;
        }

        if (health > 0)
        {
            foreach (ItemDataAmountProbability probability in _probabilityDrop)
            {
                GameObject itemDropped = Instantiate(ListAllItems.instance.listeallItems[probability.Item.id].Icon);
                while (PercentHealthLost >= 0.1f)
                {
                    itemDropped.GetComponent<Item>().amount += probability.amountEach10Percentage;
                    PercentHealthLost -= 0.1f;
                }
                probability.amountTotal -= itemDropped.GetComponent<Item>().amount;
                Inventory.instance.AddtoInventory(itemDropped);
            }
        }
        else
        {
            foreach (ItemDataAmountProbability probability in _probabilityDrop)
            {
                GameObject itemDropped = Instantiate(ListAllItems.instance.listeallItems[probability.Item.id].Icon);
                itemDropped.GetComponent<Item>().amount = probability.amountTotal;
                Inventory.instance.AddtoInventory(itemDropped);
            }
            Destroy(gameObject);
        }

    }


    public T GetObjectStat<T>() where T : StatScriptableObject
    {
        foreach (var stat in _ObjectStats)
        {
            if (stat is T cast)
            {
                return cast;
            }
        }

        return null;
    }

    public bool TryGetObjectStat<T>(out T stat) where T : StatScriptableObject
    {
        stat = GetObjectStat<T>();
        return stat != null;
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
