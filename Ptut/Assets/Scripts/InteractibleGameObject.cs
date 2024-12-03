using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGameObject : MonoBehaviour,InteractibleObject
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;


    public bool Interact(Interaction interaction)
    {
        return true;
    }

}
