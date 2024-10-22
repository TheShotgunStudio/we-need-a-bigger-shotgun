using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/EnemyStats")]
public class EnemyStats : Stats
{
    public float AttackSpeed;
    public float AttackRange;
    public float ColliderDamage;
}
