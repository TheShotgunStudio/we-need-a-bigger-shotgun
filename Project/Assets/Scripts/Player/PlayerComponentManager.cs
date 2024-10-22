using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of components attached to the player, excluding PlayerController as that would violate dependency inversion
/// Mainly used for PlayerController to pass dependencies to its state children
/// </summary>
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerComponentManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerInputHandler InputHandler;
    public CameraController CameraController;
    public Rigidbody Rigidbody;
    public Transform PlayerModel;
    public Transform PlayerSpine;
    [SerializeField]
    [HideInInspector]
    public PlayerStats BaseStats;
    public PlayerStats Stats;

    void OnEnable()
    {
        if (CameraController == null)
        {
            throw new NullReferenceException("CameraController not set for PlayerController.");
        }
        if (Stats == null)
        {
            Stats = (PlayerStats)BaseStats.Clone();
        }

        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody>();
    }
}
