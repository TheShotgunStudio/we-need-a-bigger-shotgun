using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public GameObject Player;
    protected NavMeshAgent Agent;
    public float Acceleration;
    [SerializeField]
    private EnemyStats _baseStats;
    [HideInInspector]
    public EnemyStats Stats;
    protected float AttackCooldown;

    protected virtual void Awake()
    {
        Stats = (EnemyStats)_baseStats.Clone();
        AttackCooldown = 1f / Stats.AttackSpeed;

        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Stats.Speed;
        Agent.acceleration = Stats.Acceleration;
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

    protected void ResetSpeed()
    {
        Agent.speed = Stats.Speed;
    }
}
