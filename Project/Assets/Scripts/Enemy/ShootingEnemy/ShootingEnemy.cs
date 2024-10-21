using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingEnemy : BasicEnemy
{
    public GameObject Bullet;
    public float AttackSpeed = 0.5f;


    /// <summary>
    /// Starts the movement behavior of the enemy by resetting its speed.
    /// This function is called via the MovementCollider OnTriggerExit UnityEvents.
    /// </summary>
    public void StartMovment()
    {
        Debug.Log("movenent entering");
        base.Resetspeed(); // Reset base behavior
    }

    /// <summary>
    /// Stops the movement of the enemy by setting its speed and velocity to zero.
    /// This function is called via the MovementCollider OnTriggerEnter UnityEvents.
    /// </summary>
    public void StopMovment()
    {
        Debug.Log("movenent exit");
        Agent.speed = 0;
        Agent.velocity = Vector3.zero;
    }

    /// <summary>
    /// Initiates the shooting behavior of the enemy by repeatedly invoking the Shoot method at intervals defined by AttackSpeed.
    /// This function is called via the ShootingCollider OnTriggerEnter UnityEvents.
    /// </summary>
    public void StartShooting()
    {
        Debug.Log("shooting enter");
        InvokeRepeating("Shoot", AttackSpeed, AttackSpeed);
    }

    /// <summary>
    /// Stops the shooting behavior by canceling the repeated invocation of the Shoot method.
    /// This function is called via the ShootingCollider OnTriggerExit UnityEvents.
    /// </summary>
    public void StopShooting()
    {
        Debug.Log("shooting exit");
        CancelInvoke("Shoot");
    }

    /// <summary>
    /// Shoots a bullet towards the player. The bullet is instantiated, given a forward velocity, and destroyed after 3 seconds.
    /// </summary>
    private void Shoot()
    {
        transform.LookAt(Player.transform);
        GameObject shootBullet = Instantiate(Bullet, new Vector3(0, 0.5f, 0.7f), Quaternion.Euler(0, -90, 0));
        shootBullet.transform.SetParent(this.transform, false);
        shootBullet.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
        Destroy(shootBullet, 3f);
    }
}
