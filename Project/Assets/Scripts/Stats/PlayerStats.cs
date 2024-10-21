using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/PlayerStats")]
public class PlayerStats : Stats
{
    public float Speed = 6.0f;
    public float Acceleration = 1.0f;
    public float TurnTime = 0.1F;
    public float CritRate;
    public float CritDamage = 1.5f;
    public float KnockbackForce;
}
