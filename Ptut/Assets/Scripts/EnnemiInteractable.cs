using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class EnnemiInteractable : InteractableBase
{
    public List<ItemDataAmountMinMaxDrop> dataAmountMinMaxDrop = new List<ItemDataAmountMinMaxDrop>();
    public Animator animator;
    private bool isDead
    {
        get
        {
            return health <= 0;
        }
    }
    private float TimeAnimDeath = 4f;
    private bool canAttack = true;
    private void Awake()
    {
        gameObject.layer = 10;
    }

    public override void GotHit(Item item)
    {
        if (canAttack)
        {
            WeaponStat damage;
            if (item != null && item.TryGetStat<WeaponStat>(out damage))
            {
                health -= damage.attackPower;
            }
            else
            {
                health -= 1;
            }
            if (isDead)
            {
                foreach (var item1 in dataAmountMinMaxDrop)
                {
                    int DroppedAmmount = Random.Range(item1.amountMin, item1.amountMax + 1);
                    if (DroppedAmmount > 0)
                    {
                        Inventory.instance.AddtoInventory(ListAllItems.CreateIcon(item1.Item.id,DroppedAmmount));
                    }
                }
                StartCoroutine(death());
            }
        }
    }
    IEnumerator death()
    {
        canAttack = false;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(TimeAnimDeath);
        Destroy(gameObject);
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
