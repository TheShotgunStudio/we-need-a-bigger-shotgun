using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/PlayerStats")]
public class PlayerStats : Stats
{
    public int CritRate;
    public float CritDamage = 1.5f;
    public float KnockbackForce;
    public float MoveSpeed = 3.0f; 
}
