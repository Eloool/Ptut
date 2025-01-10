using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : Stats
{
    public int health;

    private void Start()
    {
        health = GetStat<HealthStat>().health;
    }

    virtual public void GotHit(Item item)
    {

    }
}
