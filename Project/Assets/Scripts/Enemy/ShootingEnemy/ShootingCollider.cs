using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingCollider : MonoBehaviour
{
    public UnityEvent OnShootingEnter;
    public UnityEvent OnShootingExit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            OnShootingEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            OnShootingExit.Invoke();
        }
    }
}
