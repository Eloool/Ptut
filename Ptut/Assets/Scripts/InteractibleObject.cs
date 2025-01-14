using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractibleObject { 

    public string InteractionPrompt { get; }
    virtual public bool Interact() {
        return false;
    }

}
