using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testKeyBinds : MonoBehaviour
{
    public InputManagerEntry[] keys;
    // Start is called before the first frame update
    void Start()
    {
        keys[0].altBtnPositive.Replace(keys[0].altBtnPositive,"E");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
