using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactibleMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private InteractibleGameObject _interactable; 

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius,_colliders,_interactibleMask);
        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<InteractibleGameObject>();
            if (_interactable != null)
            {
                _interactionPromptUI = _interactable.GetComponent<InteractionPromptUI>();
                if (!_interactionPromptUI.IsDisplayed)_interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                if (Keyboard.current.eKey.wasPressedThisFrame) _interactable.Interact(this);
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI != null)
            {
                if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
                _interactionPromptUI = null;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionPoint.position,_interactionPointRadius);
    }
}
