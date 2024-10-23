using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BasicEnemy
{
    // Start is called before the first frame update

    public GameObject Melee;
    public float AttackDuration  = 1f;
    public float ResetDuration = 1f;
    private Vector3 _startLocation;
    private Vector3 _endLocation;



     private void Start()
    {
        _startLocation = Melee.transform.localPosition;
        _endLocation = new Vector3(Melee.transform.localPosition.x, Melee.transform.localPosition.y, 0.8f);

    }


    /// <summary>
    /// Starts the movement behavior of the enemy by resetting its speed.
    /// This function is called via the MovementCollider OnTriggerExit UnityEvents.
    /// </summary>
    public void StartMovment()
    {
        base.ResetSpeed(); // Reset base behavior
    }

    /// <summary>
    /// Stops the movement of the enemy by setting its speed and velocity to zero.
    /// This function is called via the MovementCollider OnTriggerEnter UnityEvents.
    /// </summary>
    public void StopMovment()
    {
        Agent.speed = 0;
        Agent.velocity = Vector3.zero;
    }

    /// <summary>
    /// Initiates the shooting behavior of the enemy by repeatedly invoking the Shoot method at intervals defined by AttackSpeed.
    /// This function is called via the ShootingCollider OnTriggerEnter UnityEvents.
    /// </summary>
    public void StartAttacking()
    {
        InvokeRepeating("Attack", 0, AttackDuration + ResetDuration);
    }

    /// <summary>
    /// Stops the shooting behavior by canceling the repeated invocation of the Shoot method.
    /// This function is called via the ShootingCollider OnTriggerExit UnityEvents.
    /// </summary>
    public void StopAttacking()
    {
        CancelInvoke("Attack");
    }


    [ContextMenu("attack")]
    private void Attack()
    {
        StartCoroutine(PerformAttack());
    }


    private IEnumerator PerformAttack()
    {
        transform.LookAt(Player.transform);
        float TimeCount = 0.0f;

        while (TimeCount < AttackDuration) 
        {

            Melee.transform.localPosition = Vector3.Lerp(_startLocation, _endLocation, TimeCount/AttackDuration);
            TimeCount += Time.deltaTime;
            yield return null;
        }

        Melee.transform.localPosition = _endLocation;

        TimeCount = 0.0f;
        while (TimeCount < ResetDuration)
        {

            Melee.transform.localPosition = Vector3.Lerp(_endLocation, _startLocation, TimeCount/ResetDuration);
            TimeCount += Time.deltaTime;
            yield return null;
        }

        Melee.transform.localPosition = _startLocation;

    }
}
