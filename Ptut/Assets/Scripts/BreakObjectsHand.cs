using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObjectsHand : InteractionHandBase
{
    public void BreakObjects(Item interactable)
    {
        foreach (var item in _interactableList)
        {
            item.GotHit(interactable);
        }
    }
}
