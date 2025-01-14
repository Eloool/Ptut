using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuelStat", menuName = "Stats/FuelStat")]
public class FuelStat : StatScriptableObject
{
    public int fuelParTick;
    public int maxFuel;
}