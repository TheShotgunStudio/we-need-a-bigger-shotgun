using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingEnemy : BasicEnemy
{
    public GameObject Bullet;
    public float AttackSpeed = 1.5f;



    public void StartMovment()
    {
        Debug.Log("movenent entering");
        base.Resetspeed(); // Reset base behavior
    }

    public void StopMovment()
    {
        Debug.Log("movenent exit");
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;
    }

    public void StartShooting()
    {
        Debug.Log("shooting enter");
        InvokeRepeating("Shoot", 0.05f, 0.05f);
    }

    public void StopShooting()
    {
        Debug.Log("shooting exit");
        CancelInvoke("Shoot");
    }

    /// <summary>
    /// Shoots a bullet towards the player.
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
