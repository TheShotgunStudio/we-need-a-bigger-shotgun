using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Transform _interactionPoint;
    [SerializeField]
    private float _interactionPointRadius;
    [SerializeField]
    private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    private int _collidersFound;

    private void Update()
    {
        _collidersFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_collidersFound <= 0) return;

        if (_colliders[0].TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactable.Used) return;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
