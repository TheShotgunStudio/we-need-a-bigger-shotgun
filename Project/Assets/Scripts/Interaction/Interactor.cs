using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private TextMeshProUGUI _currentPrompt;

    private void Update()
    {
        _collidersFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_collidersFound <= 0)
        {
            DeactivateCurrentPrompt();
            return;
        }

        if (_colliders[0].TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactable.Used) return;

            // Get the prompt from the Interactable
            TextMeshProUGUI prompt = _colliders[0].gameObject.GetComponentInChildren<TextMeshProUGUI>();

            if (prompt != _currentPrompt)
            {
                // Deactivate the currently active prompt if it exists
                if (_currentPrompt != null)
                {
                    _currentPrompt.enabled = false;
                }

                // Activate the new prompt
                prompt.enabled = true;
                _currentPrompt = prompt;
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
                DeactivateCurrentPrompt();
            }
        }

    }

    private void DeactivateCurrentPrompt()
    {
        if (_currentPrompt == null) return;

        _currentPrompt.enabled = false;
        _currentPrompt = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
