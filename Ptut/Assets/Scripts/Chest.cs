using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,InteractibleObject
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interaction interaction)
    {
        Debug.Log("Opening Chest");
        return true;
    }
}
