using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractibleGameObject
{
    public override bool Interact()
    {
        Inventory.instance.AddtoInventorybyItem3d(this.gameObject);
        Destroy(gameObject);
        return true;
    }
}
