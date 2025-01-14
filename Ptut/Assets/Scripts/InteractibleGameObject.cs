using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGameObject : MonoBehaviour,InteractibleObject
{
    public string _prompt;
    public string InteractionPrompt => _prompt;

    virtual public bool Interact()
    {
        return false;
    }


}
