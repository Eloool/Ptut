using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatScriptableObject : ScriptableObject
{
    public string statName;
}

[CreateAssetMenu(fileName = "NewHealthStat", menuName = "Stats/HealthStat")]
public class HealthStat : StatScriptableObject
{
    public int health;
}

[CreateAssetMenu(fileName = "NewManaStat", menuName = "Stats/ManaStat")]
public class ManaStat : StatScriptableObject
{
    public int mana;
}

[CreateAssetMenu(fileName = "NewArmorStat", menuName = "Stats/ArmorStat")]
public class ArmorStat : StatScriptableObject
{
    public int defense;
}

[CreateAssetMenu(fileName = "NewWeaponStat", menuName = "Stats/WeaponStat")]
public class WeaponStat : StatScriptableObject
{
    public int attackPower;
}

