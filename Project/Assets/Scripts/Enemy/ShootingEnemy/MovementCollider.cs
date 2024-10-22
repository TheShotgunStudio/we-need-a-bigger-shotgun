using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementCollider : MonoBehaviour
{
    public UnityEvent OnMoventEnter;
    public UnityEvent OnMoventExit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnMoventEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnMoventExit.Invoke();
        }
    }
}
