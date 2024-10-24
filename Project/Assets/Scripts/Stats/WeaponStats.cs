using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/WeaponStats")]
public class WeaponStats : Stats
{
    public float AttackSpeed;
    public float ReloadTime;
    public float ChargeTime;
    public float Range;
    public float DamageDropoffRange;
    public float RecoilStrength;
}
