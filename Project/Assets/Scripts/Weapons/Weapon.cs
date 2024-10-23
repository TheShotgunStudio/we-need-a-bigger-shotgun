using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStats _baseStats;
    [HideInInspector]
    public WeaponStats Stats;
    protected abstract void Shoot(Rigidbody playerRigidbody, CameraController cameraController);
    public abstract bool CanAttack();
    public virtual void TryShoot(Rigidbody playerRigidbody, CameraController cameraController)
    {
        if (CanAttack())
        {
            Shoot(playerRigidbody, cameraController);
        }
    }

    private void Start()
    {
        Stats = (WeaponStats)_baseStats.Clone();
    }
}
