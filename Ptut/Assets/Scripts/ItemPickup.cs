using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractibleGameObject
{
    public new bool Interact(Interaction interaction)
    {
        interaction.GetComponent<ListeItems>().AddtoInventory(GetComponent<Item3d>().IconItem);
        Destroy(gameObject);
        return true;
    }
}
