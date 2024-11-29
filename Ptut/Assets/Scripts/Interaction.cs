using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactibleMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius,_colliders,_interactibleMask);

        if (_numFound > 0)
        {
            var _interactible = _colliders[0].GetComponent<InteractibleObject>();

            if (_interactible != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                _interactible.Interact(this);
            }
        }
    }


}
