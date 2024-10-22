using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingEnemy : BasicEnemy
{
    public GameObject Bullet;
    public float BulletSpeed = 25f;
    public float TimeBetweenBullets = 0.5f;


    /// <summary>
    /// Starts the movement behavior of the enemy by resetting its speed.
    /// This function is called via the MovementCollider OnTriggerExit UnityEvents.
    /// </summary>
    public void StartMovment()
    {
        base.Resetspeed(); // Reset base behavior
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
    public void StartShooting()
    {
        InvokeRepeating("Shoot", TimeBetweenBullets, TimeBetweenBullets);
    }

    /// <summary>
    /// Stops the shooting behavior by canceling the repeated invocation of the Shoot method.
    /// This function is called via the ShootingCollider OnTriggerExit UnityEvents.
    /// </summary>
    public void StopShooting()
    {
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
        shootBullet.GetComponent<Rigidbody>().velocity = transform.forward * BulletSpeed;
        Destroy(shootBullet, 3f);//this has to change, this is only for the prototype
    }
}
