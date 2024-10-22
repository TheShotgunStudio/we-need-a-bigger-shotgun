using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    public float CurrentHealth { get { return _currentHealth; } }

    private void Start()
    {
        if (TryGetComponent(out PlayerController player))
        {
            _maxHealth = player.PlayerComponentManager.Stats.Health;
        }
        else if (TryGetComponent(out BasicEnemy enemy))
        {
            _maxHealth = enemy.Stats.Health;
        }

        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Heal the entity
    /// </summary>
    /// <param name="healingAmount">Amount of health that gets healed</param>
    public void Heal(float healingAmount)
    {
        _currentHealth += healingAmount;

        if (_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Deal damage to the entity
    /// </summary>
    /// <param name="damageAmount">Amount of damage that has to be dealt</param>
    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
