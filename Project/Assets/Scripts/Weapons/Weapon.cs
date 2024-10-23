using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStats _baseStats;
    [HideInInspector]
    public WeaponStats Stats;
    public abstract void Shoot();

    private void Start()
    {
        Stats = (WeaponStats)_baseStats.Clone();
    }
}
