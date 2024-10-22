using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementCollider : MonoBehaviour
{
    public UnityEvent OnMovementEnter;
    public UnityEvent OnMovementExit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnMovementEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnMovementExit.Invoke();
        }
    }
}
