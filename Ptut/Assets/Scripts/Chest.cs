using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractibleGameObject
{
    public new bool Interact()
    {
        Debug.Log("Opening Chest");
        return true;
    }
}
