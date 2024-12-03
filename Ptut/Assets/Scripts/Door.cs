using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractibleGameObject
{ 
    public new bool Interact(Interaction interaction)
    {
        Debug.Log("Opening Door");
        return true;
    }
}
