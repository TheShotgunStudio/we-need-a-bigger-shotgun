using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

/// <summary>
/// Player movement and interaction input handler.
/// </summary>
/// <remarks>
/// Not universal for all input, but keeps track of important information like the direction of input and what that translates to in world space.
/// </remarks>
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    public delegate void InputDelegate(InputValue value);

    /// <summary>
    /// Current direction of input in world space
    /// </summary>
    public Vector3 CurrentMovementDirection { get; set; } = Vector3.zero;

    /// <summary>
    /// Current direction of input in input space
    /// </summary>
    public Vector3 CurrentInputDirection { get; set; } = Vector3.zero;

    /// <summary>
    /// Current angle at which the player character would be aligned with the movement direction
    /// </summary>
    public float CurrentTargetAngle { get; set; }

    /// <summary>
    /// Whether the camera can currently be controlled
    /// </summary>
    public bool CanMoveCamera { get; set; }

    /// <summary>
    /// List of delegates to be called when an input is received
    /// </summary>
    public List<InputDelegate> OnMoveDelegates { get; private set; } = new List<InputDelegate>();

    /// <summary>
    /// Called when a movement direction is held down
    /// </summary>
    /// <param name="value">Generic input value</param>
    public void OnMove(InputValue value)
    {
        // Get the input
        Vector2 input = value.Get<Vector2>();

        // Convert it to a vector3
        CurrentInputDirection = GetInputDirection(input);

        foreach (InputDelegate inputDelegate in OnMoveDelegates)
        {
            inputDelegate.Invoke(value);
        }
    }

    public void Update()
    {
        // If a direction is being held down
        if (CurrentInputDirection.magnitude > 0.01F)
        {
            // Face that direction
            float targetAngle = Mathf.Atan2(CurrentInputDirection.x, CurrentInputDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            CurrentMovementDirection = Quaternion.Euler(0F, targetAngle, 0F) * Vector3.forward;
        }
        else
        {
            CurrentMovementDirection = Vector3.zero;
        }

        // And calculate the resulting target angle 
        CurrentTargetAngle = Mathf.Atan2(CurrentInputDirection.x, CurrentInputDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        
    }

    /// <summary>
    /// List of delegates to be called when an input is received
    /// </summary>
    public List<InputDelegate> OnAttackDelegates { get; private set; } = new List<InputDelegate>();

    /// <summary>
    /// Called when the player presses the attack button
    /// </summary>
    /// <param name="value">Generic input value</param>
    public void OnAttack(InputValue value)
    {
        foreach (InputDelegate inputDelegate in OnAttackDelegates)
        {
            inputDelegate.Invoke(value);
        }
    }

    /// <summary>
    /// List of delegates to be called when an input is received
    /// </summary>
    public List<InputDelegate> OnJumpDelegates { get; private set; } = new List<InputDelegate>();

    /// <summary>
    /// Called when the player presses the jump button
    /// </summary>
    /// <param name="value">Generic input value</param>
    public void OnJump(InputValue value)
    {
        foreach (InputDelegate inputDelegate in OnJumpDelegates)
        {
            inputDelegate.Invoke(value);
        }
    }

    /// <summary>
    /// Gets the current direction of input in input space
    /// </summary>
    /// <returns></returns>
    private Vector3 GetInputDirection(Vector2 input)
    {
        float horizontal = input.x;
        float vertical = input.y;
        return new Vector3(horizontal, 0F, vertical).normalized;
    }
}
