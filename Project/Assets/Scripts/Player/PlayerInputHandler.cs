using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player movement and interaction input handler.
/// </summary>
/// <remarks>
/// Not universal for all input, but keeps track of important information like the direction of input and what that translates to in world space.
/// </remarks>
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
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
    /// Called when a movement direction is held down
    /// </summary>
    /// <param name="value">Generic input value</param>
    public void OnMove(InputValue value)
    {
        // Get the input
        Vector2 input = value.Get<Vector2>();

        // Convert it to a vector3
        CurrentInputDirection = GetInputDirection(input);

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
