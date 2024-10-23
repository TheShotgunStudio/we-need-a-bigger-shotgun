using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/PlayerStats")]
public class PlayerStats : Stats
{
    public float Acceleration;
    public float TurnTime;
    public float AirTurning;
    public float CritRate;
    public float CritDamage;
    public float Drag;
    public float SpeedCap;
}
