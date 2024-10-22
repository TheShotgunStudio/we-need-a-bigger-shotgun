using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public GameObject Player;
    protected NavMeshAgent Agent;

    public float Speed;
    private float _speedSave;
    public float Acceleration;

    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;
        Agent.acceleration = Acceleration;
        _speedSave = Speed;
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    /// <summary>
    /// Moves the enemy towards the player.
    /// </summary>
    protected void MoveToPlayer()
    {
        Agent.SetDestination(Player.transform.position);
    }

    /// <summary>
    /// Handles the enemy's death behavior.
    /// </summary>
    protected virtual void Death()
    {
        // Implement enemy death behavior (generic)
        Debug.Log("Enemy died");
    }

    protected void Resetspeed()
    {
        Agent.speed = _speedSave;
    }
}
