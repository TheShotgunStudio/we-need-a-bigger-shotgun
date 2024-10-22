using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Main player script. Functions as a finite state machine to delegate responsibilities.
/// </summary>
[RequireComponent(typeof(PlayerComponentManager))]
public class PlayerController : MonoBehaviour, IFiniteStateMachine, IAttackHandler
{
    public CameraController CameraController;
    public Dictionary<string, IAbstractState> States { get; private set; }
    public IAbstractState CurrentState { get; private set; }
    public PlayerComponentManager PlayerComponentManager { get; private set; }
    [SerializeField]
    private PlayerStats _baseStats;
    public PlayerStats Stats { get; private set; }

    /// <summary>
    /// A list of layers this player controller will treat as ground/terrain
    /// </summary>
    public LayerMask GroundLayerMask;

    public void SetState(IAbstractState state)
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

    void Awake() { // make sure the state are initiated when needed
        if (Stats == null)
        {
            Stats = (PlayerStats)_baseStats.Clone();
        }
    }

    void Start()
    {
        if (CameraController == null)
        {
            throw new NullReferenceException("CameraController not set for PlayerController.");
        }
        
        PlayerComponentManager = GetComponent<PlayerComponentManager>();
        InitializeStates();

        PlayerComponentManager.InputHandler.OnAttackDelegates.Add((value) => OnAttackInput(value));
    }

    public void InitializeStates()
    {
        IFiniteStateMachine.StateSetter stateSetter = (state) => CurrentState = state;

        // Initialize state dictionary
        States = new Dictionary<string, IAbstractState>()
        {
            { "Movable", new MoveState(stateSetter, PlayerComponentManager, CameraController, Stats) }
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

    public void OnAttackInput(InputValue value)
    {
        if (CurrentState is not IAttackHandler) return;
        ((IAttackHandler)CurrentState).OnAttackInput(value);
    }
    
    public void ApplyUpgrade(UpgradeData upgrade)
    {
        float increase;

        switch (upgrade.UpgradeName)
        {
            case "Health":
                increase = _baseStats.Health * upgrade.Value;
                Stats.Health += increase;
                break;
            case "Attack":
                increase = _baseStats.Attack * upgrade.Value;
                Stats.Attack += increase;
                break;
            case "Speed":
                increase = _baseStats.Speed * upgrade.Value;
                Stats.Speed += increase;
                break;
        }
    }
}
