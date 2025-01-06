using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionnairePeripherique : MonoBehaviour
{
    [SerializeField]
    private Vector2 deplacement; 

    private PeripheriqueEntree peripheriqueEntree;

    private void Awake()
    {
        peripheriqueEntree = new PeripheriqueEntree();

        peripheriqueEntree.Player.Move.performed += LireDep1acement;
        peripheriqueEntree.Player.Move.canceled += LireDep1acement;
    }

    private void LireDep1acement(InputAction.CallbackContext context)
    {
        deplacement = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        peripheriqueEntree.Enable();
    }

    private void OnDisable()
    {
        peripheriqueEntree.Player.Disable();
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
