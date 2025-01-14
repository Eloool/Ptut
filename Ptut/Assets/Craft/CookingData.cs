using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCookingData", menuName = "Cooking/New Cooking")]
public class CookingData : ScriptableObject
{
    public ItemDataAndAmount ItemGotbyCooking;
    public ItemDataAndAmount ItemsNeededforCooking;
}
