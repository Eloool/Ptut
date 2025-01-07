using UnityEngine;
using UnityEngine.InputSystem;

public class GestionnairePeripherique : MonoBehaviour
{
    private Vector2 _deplacement;
    private Vector2 _watch;
    private bool _jumpOn;
    private bool _sprintOn;
    private bool _invintoryOn;
    private bool _clickLeftOn;
    private bool _clickRightOn;

    // Propriétés publiques
    public Vector2 Deplacement => _deplacement;
    public Vector2 Watch => _watch;
    public bool JumpOn => _jumpOn;
    public bool SprintOn => _sprintOn;
    public bool InvintoryOn => _invintoryOn;
    public bool ClickLeftOn => _clickLeftOn;
    public bool ClickRightOn => _clickRightOn;

    private PeripheriqueEntree peripheriqueEntree;
    private bool _initialized = false;

    private void Awake()
    {
        try
        {
            peripheriqueEntree = new PeripheriqueEntree();

            peripheriqueEntree.Player.Move.performed += LireDep1acement;
            peripheriqueEntree.Player.Move.canceled += LireDep1acement;

            peripheriqueEntree.Player.Look.performed += LookPlayer;
            peripheriqueEntree.Player.Look.canceled += LookPlayer;

            peripheriqueEntree.Player.Jump.performed += ReadJump;
            peripheriqueEntree.Player.Jump.canceled += ReadJump;

            peripheriqueEntree.Player.Sprint.performed += ReadSprint;
            peripheriqueEntree.Player.Sprint.canceled += ReadSprint;

            peripheriqueEntree.Player.Invintory.performed += ReadInvintory;
            peripheriqueEntree.Player.Invintory.canceled += ReadInvintory;

            peripheriqueEntree.Player.ClickLeft.performed += ReadClickLeft;
            peripheriqueEntree.Player.ClickLeft.canceled += ReadClickLeft;

            peripheriqueEntree.Player.ClickRight.performed += ReadClickRight;
            peripheriqueEntree.Player.ClickRight.canceled += ReadClickRight;

            _initialized = true;
            Debug.Log("GestionnairePeripherique initialisé avec succès");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erreur lors de l'initialisation du GestionnairePeripherique: {e.Message}");
        }
    }

    private void OnEnable()
    {
        if (_initialized)
        {
            peripheriqueEntree.Enable();
            Debug.Log("Périphériques d'entrée activés");
        }
    }

    private void OnDisable()
    {
        if (_initialized)
        {
            peripheriqueEntree.Disable();
            Debug.Log("Périphériques d'entrée désactivés");
        }
    }
    private void LireDep1acement(InputAction.CallbackContext context)
    {
        _deplacement = context.ReadValue<Vector2>();
    }

    private void LookPlayer(InputAction.CallbackContext context)
    {
        _watch = context.ReadValue<Vector2>();
    }

    private void ReadJump(InputAction.CallbackContext context)
    {
        _jumpOn = context.ReadValueAsButton();
    }

    private void ReadSprint(InputAction.CallbackContext context)
    {
        _sprintOn = context.ReadValueAsButton();
    }

    private void ReadInvintory(InputAction.CallbackContext context)
    {
        _invintoryOn = context.ReadValueAsButton();
    }

    private void ReadClickLeft(InputAction.CallbackContext context)
    {
        _clickLeftOn = context.ReadValueAsButton();
    }

    private void ReadClickRight(InputAction.CallbackContext context)
    {
        _clickRightOn = context.ReadValueAsButton();
    }

}