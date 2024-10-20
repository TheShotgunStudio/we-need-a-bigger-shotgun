using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

/// <summary>
/// Main player script. Functions as a finite state machine to delegate responsibilities.
/// </summary>
[RequireComponent(typeof(PlayerComponentManager))]
public class PlayerController2 : MonoBehaviour, FiniteStateMachine
{
    public Dictionary<string, AbstractState> States { get; private set; }
    public AbstractState CurrentState { get; private set; }
    public PlayerComponentManager PlayerComponentManager { get; private set; }

    /// <summary>
    /// A list of layers this player controller will treat as ground/terrain
    /// </summary>
    public LayerMask GroundLayerMask;

    public void SetState(AbstractState state)
    {
        if (state == null) return;
        if (CurrentState != null) CurrentState.OnStateExit();
        CurrentState = state;
        CurrentState.OnStateEnter();
    }

    public void SetState(string stateKey)
    {
        SetState(States[stateKey]);
    }

    void Start()
    {
        PlayerComponentManager = GetComponent<PlayerComponentManager>();
        InitializeStates();
    }

    public void InitializeStates()
    {
        FiniteStateMachine.StateSetter stateSetter = (state) => CurrentState = state;

        // Initialize state dictionary
        States = new Dictionary<string, AbstractState>()
        {
            { "Movable", new MoveState(stateSetter, PlayerComponentManager) }
        };

        // Activate state machine by setting the default state
        SetState("Movable");
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.OnStateProcessing();
    }
    
    // FixedUpdate is called a fixed amount of times per second
    void FixedUpdate()
    {
        CurrentState.OnStateFixedProcessing();
    }
}
