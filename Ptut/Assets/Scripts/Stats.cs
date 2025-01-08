using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats :MonoBehaviour
{
    [SerializeField]
    private List<StatScriptableObject> StatsScripts;

    public T GetStat<T>() where T : StatScriptableObject
    {
        foreach (var stat in StatsScripts)
        {
            if (stat is T cast)
            {
                return cast;
            }
        }

        return null;
    }

    public bool TryGetStat<T>(out T stat) where T : StatScriptableObject
    {
        stat = GetStat<T>();
        return stat != null;
    }
}
