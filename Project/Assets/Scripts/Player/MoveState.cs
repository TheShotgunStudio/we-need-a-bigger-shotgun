using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// In this state the player can be moved around by holding a direction.
/// </summary>
public class MoveState : ControlState, IAttackHandler
{
    private PlayerComponentManager _playerComponentManager;
    private GameObject _playerObject;
    private Rigidbody _playerRigidbody;
    private PlayerInputHandler _playerInputHandler;
    private CameraController _cameraController;
    private PlayerStats _playerStats;
    private LayerMask _layerMask;
    private Transform _playerModel;
    private Transform _playerSpine;

    private float _turnSmoothVelocity;
    private float _playerHeight = 2.2F;

    public Dictionary<string, IAbstractState> Neighbors { get; private set; } = new Dictionary<string, IAbstractState>();
    public IFiniteStateMachine.StateSetter StateSetter { get; private set; }

    public MoveState(IFiniteStateMachine.StateSetter stateSetter, PlayerComponentManager playerComponentManager, CameraController cameraController, PlayerStats playerStats, LayerMask layerMask)
    {
        this._playerComponentManager = playerComponentManager;
        this._playerObject = playerComponentManager.gameObject;
        this._playerRigidbody = playerComponentManager.Rigidbody;
        this._playerInputHandler = playerComponentManager.InputHandler;
        this._playerModel = playerComponentManager.PlayerModel;
        this._playerSpine = playerComponentManager.PlayerSpine; 
        this._cameraController = cameraController;
        this._playerStats = playerStats;
        this._layerMask = layerMask;
        this.StateSetter = stateSetter;
    }

    public object Clone()
    {
        MoveState moveState = new MoveState(StateSetter, _playerComponentManager, _cameraController, _playerStats, _layerMask);
        moveState._turnSmoothVelocity = _turnSmoothVelocity;

        return moveState;
    }

    // Called a fixed amount of times per second if this is the current state of the FSM
    public void OnStateFixedProcessing()
    {
        // Handle camera
        _cameraController.CameraFollowTarget.transform.position = _playerObject.transform.position;

        Vector3 velocity = _playerRigidbody.velocity;
        Vector3 position = _playerObject.transform.position;

        // Preserve the y component
        float y = velocity.y;

        //Move player model and its spine according to the camera
        _playerModel.transform.rotation = Quaternion.Euler(0,  Camera.main.transform.rotation.eulerAngles.y, 0);
        Quaternion lookRotation = Quaternion.LookRotation((Camera.main.transform.position + Camera.main.transform.forward * 10.0f) - _playerSpine.position);
        _playerSpine.rotation = math.slerp(_playerSpine.rotation, lookRotation, 0.9f);


        // If the player is holding down a direction
        if (_playerInputHandler.CurrentInputDirection.magnitude > 0.01)
        {
            // Gradually rotate the player character to face that direction
            float angle = Mathf.SmoothDampAngle(_playerObject.transform.eulerAngles.y, _playerInputHandler.CurrentTargetAngle, ref _turnSmoothVelocity, _playerStats.TurnTime);
            _playerObject.transform.rotation = Quaternion.Euler(0F, angle, 0F);

            MovePlayer(position, ref velocity);
        } else if (velocity.magnitude < _playerStats.Speed)
        {
            // Apply strong drag to the movement
            float drag = 30.0F;
            velocity -= velocity.normalized * Mathf.Clamp(Time.deltaTime * _playerStats.Acceleration * drag, 0.0F, velocity.magnitude);
        }

        // Reintroduce the Y component to the velocity
        _playerRigidbody.velocity = new Vector3(velocity.x, y, velocity.z);
    }

    /// <summary>
    /// Handles actual player movement
    /// </summary>
    public void MovePlayer(Vector3 position, ref Vector3 velocity)
    {
        if (velocity.magnitude <= _playerStats.Speed && IsGrounded(position))
        {
            MoveSnappy(ref velocity);
        }
        else
        {
            MoveWithAcceleration(ref velocity);
        }
    }

    /// <summary>
    /// Moves more responsively by snapping existing movement to the new direction
    /// </summary>
    public void MoveSnappy(ref Vector3 velocity)
    {
        // Set the new velocity to be the old velocity but in the direction of the new movement input direction
        velocity = velocity.magnitude * _playerInputHandler.CurrentMovementDirection;

        // Add the acceleration to the existing velocity
        velocity += _playerInputHandler.CurrentMovementDirection * _playerStats.Speed * _playerStats.Acceleration * Time.deltaTime * 4.0F;

        // Clamp the velocity to the max speed
        velocity = Vector3.ClampMagnitude(velocity, _playerStats.Speed);
    }

    /// <summary>
    /// Moves by adding velocity based on acceleration
    /// </summary>
    public void MoveWithAcceleration(ref Vector3 velocity)
    {
        float velocityArcingMultiplier = Mathf.Max(velocity.magnitude * _playerStats.AirTurning, 1.0F);
        float accelerationRate = _playerStats.Speed * _playerStats.Acceleration * Time.deltaTime * 2.0F;
        Vector3 addedMovement = _playerInputHandler.CurrentMovementDirection * accelerationRate * velocityArcingMultiplier;
        velocity = Vector3.ClampMagnitude(new Vector3(velocity.x, 0.0F, velocity.z) + addedMovement, Mathf.Max(new Vector2(velocity.x, velocity.z).magnitude, _playerStats.Speed));
    }

    public bool IsGrounded(Vector3 position)
    {
        return Physics.Raycast(position, Vector3.down, _playerHeight * 0.5F, _layerMask);
    }

    public void OnStateProcessing()
    {
        if (_cameraController == null) return;
        _cameraController.OnUpdate();
    }

    public void OnAttackInput(InputValue value)
    {
        // Set the rigidbody velocity opposite the camera directions
        _playerRigidbody.velocity -= _cameraController.CameraFollowTarget.transform.forward * 3.0F;
    }
}
