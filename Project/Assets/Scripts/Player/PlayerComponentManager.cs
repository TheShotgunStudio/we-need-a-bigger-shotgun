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
    [HideInInspector]
    public Rigidbody Rigidbody;

    void OnEnable()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody>();
    }
}
