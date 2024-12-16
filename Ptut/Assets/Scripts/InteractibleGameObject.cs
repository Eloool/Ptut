using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGameObject : MonoBehaviour,InteractibleObject
{
    public string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact()
    {
        ListeItems.instance.AddtoInventorybyItem3d(this.gameObject);
        Destroy(gameObject);
        return true;
    }

}
