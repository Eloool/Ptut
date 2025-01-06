using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionnairePeripherique : MonoBehaviour
{
    private PeripheriqueEntree PeripheriqueEntree;

    private void Awake()
    {
        PeripheriqueEntree = new PeripheriqueEntree();
    }

    private void OnEnable()
    {
        PeripheriqueEntree.Player.enabled();
    }

    private void OnDisable()
    {
        PeripheriqueEntree.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
