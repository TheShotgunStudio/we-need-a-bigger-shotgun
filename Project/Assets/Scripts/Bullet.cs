using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            if (other.TryGetComponent<Health>(out Health healthScript)) {
                healthScript.TakeDamage(5); // TODO change to use stats Currently 1 damage
            }
        } else if (other.gameObject.CompareTag("Ground") 
                || other.gameObject.CompareTag("Wall")) 
        {
            Destroy(this.gameObject);
        }
    }
}
