using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractibleGameObject
{
    public new bool Interact()
    {
        ListeItems.instance.AddtoInventorybyItem3d(this.gameObject);
        Destroy(gameObject);
        return true;
    }
}
