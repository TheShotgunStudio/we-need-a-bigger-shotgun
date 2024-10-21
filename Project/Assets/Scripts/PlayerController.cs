using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody Rigidbody;
    [SerializeField]
    private PlayerStats _baseStats;
    [HideInInspector]
    public PlayerStats Stats;
    public GameObject CameraFollowTarget;

    private void Awake()
    {
        if (!TryGetComponent(out Rigidbody))
        {
            throw new NullReferenceException("PlayerController did not find Rigidbody component.");
        }
        if (CameraFollowTarget == null)
        {
            throw new NullReferenceException("PlayerController does not have CameraFollowTarget set.");
        }

        Stats = (PlayerStats)_baseStats.Clone();
    }
}
