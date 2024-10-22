using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/PlayerStats")]
public class PlayerStats : Stats
{
    public float Speed = 6.0F;
    public float Acceleration;
    public float TurnTime;
    public float AirTurning = 0.07F;
    public float CritRate;
    public float CritDamage;
}
