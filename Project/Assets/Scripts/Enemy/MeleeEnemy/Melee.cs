using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Melee : MonoBehaviour
{
    private float _damage;

    public void Start()
    {
        _damage = GetComponentInParent<MeleeEnemy>().GetDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Health>(out Health healthScript))
            {
                healthScript.TakeDamage(_damage);
            }
        }

    }
 
}
