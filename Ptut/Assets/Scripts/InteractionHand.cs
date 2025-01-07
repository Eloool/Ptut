using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHand : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1.0f;
    [SerializeField] private LayerMask _interactibleMask;

    private Collider[] _colliders = new Collider[60];
    [SerializeField] private int _numFound;

    private List<BreakableGameObject> _breakableList = new List<BreakableGameObject>();

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactibleMask);

        // Gérer les nouveaux objets dans la sphère
        for (int i = 0; i < _numFound; i++)
        {
            BreakableGameObject _breakable = _colliders[i].GetComponent<BreakableGameObject>();
            if (_breakable != null && !_breakableList.Contains(_breakable))
            {
                _breakableList.Add(_breakable);
            }
        }
        for (int i = _breakableList.Count - 1; i >= 0; i--)
        {
            var interactable = _breakableList[i];
            if (interactable == null || !IsInColliderArray(interactable))
            {
                _breakableList.RemoveAt(i);
            }
        }
    }

    private bool IsInColliderArray(BreakableGameObject interactable)
    {
        for (int i = 0; i < _numFound; i++)
        {
            if (_colliders[i] != null && _colliders[i].GetComponent<BreakableGameObject>() == interactable)
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

    public void BreakObjects(Item interactable)
    {
        foreach (var item in _breakableList)
        {
            item.GotHit(interactable);
        }
    }
}
