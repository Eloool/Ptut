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
    [SerializeField] private List<InteractionPromptUI> _interactionPromptUIList = new List<InteractionPromptUI>();

    private Collider[] _colliders = new Collider[60];
    [SerializeField] private int _numFound;

    private List<InteractibleGameObject> _interactableList = new List<InteractibleGameObject>();

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactibleMask);

        // Gérer les nouveaux objets dans la sphère
        for (int i = 0; i < _numFound; i++)
        {
            InteractibleGameObject _interactable = _colliders[i].GetComponent<InteractibleGameObject>();
            if (_interactable != null && !_interactableList.Contains(_interactable))
            {
                _interactableList.Add(_interactable);

                InteractionPromptUI _interactionPromptUI = _interactable.GetComponent<InteractionPromptUI>();
                if (_interactionPromptUI != null && !_interactionPromptUI.IsDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                    _interactionPromptUIList.Add(_interactionPromptUI);
                }
            }
        }

        // Gérer les objets qui ne sont plus dans la sphère
        for (int i = _interactableList.Count - 1; i >= 0; i--)
        {
            var interactable = _interactableList[i];
            if (interactable == null || !IsInColliderArray(interactable))
            {
                if (_interactionPromptUIList[i].IsDisplayed)
                {
                    _interactionPromptUIList[i].Close();
                }

                _interactionPromptUIList.RemoveAt(i);
                _interactableList.RemoveAt(i);
            }
        }

        Debug.Log($"{_numFound} {_interactableList.Count}");
    }

    // Vérifie si un objet est encore dans la liste des colliders détectés
    private bool IsInColliderArray(InteractibleGameObject interactable)
    {
        for (int i = 0; i < _numFound; i++)
        {
            if (_colliders[i] != null && _colliders[i].GetComponent<InteractibleGameObject>() == interactable)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionPoint.position,_interactionPointRadius);
    }
}
