using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStats _baseStats;
    [HideInInspector]
    public WeaponStats Stats;

    public virtual void TryShoot(Rigidbody playerRigidbody, CameraController cameraController)
    {
        if (CanShoot())
        {
            Shoot(playerRigidbody, cameraController);
        }
    }
    public abstract bool CanShoot();
    protected abstract void Shoot(Rigidbody playerRigidbody, CameraController cameraController);

    private void Start()
    {
        Stats = (WeaponStats)_baseStats.Clone();
    }
}
