using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public GameObject Player;
    protected NavMeshAgent _agent;

    public float Speed;
    private float _speedSave;
    public float Acceleration;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Speed;
        _agent.acceleration = Acceleration;
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
        _agent.SetDestination(Player.transform.position);
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
        _agent.speed = _speedSave;
    }
}
