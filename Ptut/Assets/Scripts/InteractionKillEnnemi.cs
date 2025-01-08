using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionKillEnnemi : InteractionHandBase
{
    public void HitEnnemis(Item interactable)
    {
        foreach (var item in _interactableList)
        {
            item.GotHit(interactable);
        }
    }
}
