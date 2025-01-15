using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : Stats
{
    public int health;

    private void Start()
    {
        HealthStat healthStat;
        if (TryGetStat<HealthStat>(out healthStat))
        {
            health = GetStat<HealthStat>().health;
        }
        else
        {
            health = 0;
        }
        if(health <= 0)
        {
            Debug.LogError(gameObject + " n'as pas de vie");
        }

    }

    virtual public void GotHit(Item item)
    {

    }
}
