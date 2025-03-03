using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandBase : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1.0f;
    [SerializeField] private LayerMask _interactableMask;

    private Collider[] _colliders = new Collider[60];
    private int _numFound;

    protected List<InteractableBase> _interactableList = new List<InteractableBase>();

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        // Gérer les nouveaux objets dans la sphère
        for (int i = 0; i < _numFound; i++)
        {
            InteractableBase _interactable = _colliders[i].GetComponent<InteractableBase>();
            if (_interactable != null && !_interactableList.Contains(_interactable))
            {
                _interactableList.Add(_interactable);
            }
        }
        for (int i = _interactableList.Count - 1; i >= 0; i--)
        {
            var interactable = _interactableList[i];
            if (interactable == null || !IsInColliderArray(interactable))
            {
                _interactableList.RemoveAt(i);
            }
        }
    }

    private bool IsInColliderArray(InteractableBase interactable)
    {
        for (int i = 0; i < _numFound; i++)
        {
            if (_colliders[i] != null && _colliders[i].GetComponent<InteractableBase>() == interactable)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
