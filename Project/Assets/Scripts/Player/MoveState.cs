using System.Collections;
using System.Collections.Generic;
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

    private float _turnSmoothVelocity;

    public Dictionary<string, AbstractState> Neighbors { get; private set; } = new Dictionary<string, AbstractState>();
    public FiniteStateMachine.StateSetter StateSetter { get; private set; }

    public MoveState(FiniteStateMachine.StateSetter stateSetter, PlayerComponentManager playerComponentManager, CameraController cameraController)
    {
        this._playerComponentManager = playerComponentManager;
        this._playerObject = playerComponentManager.gameObject;
        this._playerRigidbody = playerComponentManager.Rigidbody;
        this._playerInputHandler = playerComponentManager.InputHandler;
        this._cameraController = cameraController;
        this.StateSetter = stateSetter;
    }

    public object Clone()
    {
        MoveState moveState = new MoveState(StateSetter, _playerComponentManager, _cameraController);
        moveState._turnSmoothVelocity = _turnSmoothVelocity;

        return moveState;
    }

    // Called a fixed amount of times per second if this is the current state of the FSM
    public void OnStateFixedProcessing()
    {
        // Handle camera
        _cameraController.CameraFollowTarget.transform.position = _playerObject.transform.position;

        // Preserve the y component
        float y = _playerRigidbody.velocity.y;

        // If the player is holding down a direction
        if (_playerInputHandler.CurrentInputDirection.magnitude > 0.01)
        {
            // Gradually rotate the player character to face that direction
            float angle = Mathf.SmoothDampAngle(_playerObject.transform.eulerAngles.y, _playerInputHandler.CurrentTargetAngle, ref _turnSmoothVelocity, PlayerData.Stats.TurnTime);
            _playerObject.transform.rotation = Quaternion.Euler(0F, angle, 0F);

            // Holy cow, Batman, this is scuffed!
            // I have no idea why something this convoluted produces remotely good results, but hey if it works, it works
            _playerRigidbody.velocity = _playerRigidbody.velocity.magnitude * _playerInputHandler.CurrentMovementDirection;
            _playerRigidbody.velocity += _playerInputHandler.CurrentMovementDirection * PlayerData.Stats.Speed * PlayerData.Stats.Acceleration * Time.deltaTime * 4.0F;
            _playerRigidbody.velocity = Vector3.ClampMagnitude(_playerRigidbody.velocity, PlayerData.Stats.Speed);

            // Reintroduce the Y component to the velocity
            _playerRigidbody.velocity += new Vector3(0F, y, 0F);
        }
        else
        {
            // If the velocity is almost zero
            if (_playerRigidbody.velocity.magnitude < 0.1)
            {
                // Remove all horizontal velocity
                _playerRigidbody.velocity = new Vector3(0F, _playerRigidbody.velocity.y, 0F);
            } else
            {
                // Apply insane drag to the movement
                _playerRigidbody.velocity -= _playerRigidbody.velocity.normalized * Time.deltaTime * PlayerData.Stats.Acceleration * 30F;

                // Reintroduce the Y component to the velocity
                _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, y, _playerRigidbody.velocity.z);
            }
        }
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
