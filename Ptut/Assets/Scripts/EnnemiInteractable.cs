using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnnemiInteractable : InteractableBase
{
    public List<ItemDataAmountMinMaxDrop> dataAmountMinMaxDrop = new List<ItemDataAmountMinMaxDrop>(); 

    private void Awake()
    {
        gameObject.layer = 10;
    }

    public override void GotHit(Item item)
    {
        WeaponStat damage;
        if (item != null && item.TryGetStat<WeaponStat>(out damage))
        {
            health-= damage.attackPower;
        }
        else
        {
            health -= 1;
        }
        if (health <= 0)
        {
            foreach (var item1 in dataAmountMinMaxDrop)
            {
                int DroppedAmmount = Random.Range(item1.amountMin, item1.amountMax+1);
                if (DroppedAmmount > 0)
                {
                    GameObject itemDropped = Instantiate(ListAllItems.instance.listeallItems[item1.Item.id].Icon);
                    itemDropped.GetComponent<Item>().amount = DroppedAmmount;
                    Inventory.instance.AddtoInventory(itemDropped);
                }
            }
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class ItemDataAmountMinMaxDrop
{
    public ItemData Item;
    [Range(0, 10)]
    public int amountMax;
    [Range(0, 10)]
    public int amountMin;
}
