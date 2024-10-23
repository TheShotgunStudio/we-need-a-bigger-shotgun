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
    private GameObject _currentInteractableChild;

    private void Update()
    {
        _collidersFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_collidersFound <= 0)
        {
            DeactivateCurrentChild();
            return;
        }

        if (_colliders[0].TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactable.Used) return;

            // Get the child GameObject
            GameObject interactableChild = _colliders[0].transform.GetChild(0).gameObject;

            if (interactableChild != _currentInteractableChild)
            {
                // Deactivate the currently active child if it exists
                if (_currentInteractableChild != null)
                {
                    _currentInteractableChild.SetActive(false);
                }

                // Activate the new interactable child
                interactableChild.SetActive(true);
                _currentInteractableChild = interactableChild;
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
                DeactivateCurrentChild();
            }
        }

    }

    private void DeactivateCurrentChild()
    {
        if (_currentInteractableChild == null) return;

        _currentInteractableChild.SetActive(false);
        _currentInteractableChild = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
