using Cinemachine;
using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public GameObject CameraFollowTarget;
    [HideInInspector]
    public CinemachineVirtualCamera VirtualCamera;

    /// <summary>
    /// The vertical camera sensitivity
    /// </summary>
    [Range(0.0F, 2.0F)]
    public float SensitivityY = 0.5F;

    /// <summary>
    /// The horizontal camera sensitivity
    /// </summary>
    [Range(0.0F, 2.0F)]
    public float SensitivityX = 0.8F;

    /// <summary>
    /// General camera sensitivity
    /// </summary>
    [Range(0.0F, 2.0F)]
    public float GeneralCameraSensitivity = 1.0F;

    /// <summary>
    /// Flips the camera movement horizontally
    /// </summary>
    public bool InvertCameraX;

    /// <summary>
    /// Flips the camera movement vertically
    /// </summary>
    public bool InvertCameraY;

    private Vector2 _look;

    void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (VirtualCamera.Follow == null)
        {
            throw new NullReferenceException("VirtualCamera does not have Follow Target set.");
        }

        CameraFollowTarget = VirtualCamera.Follow.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float angleX = CameraFollowTarget.transform.localEulerAngles.x;

        // Maximum down and up rotation respectively
        float minY = 275.0F;
        float maxY = 80.0F;

        // Factor decreases the X sensitivity as the camera approaches the limit of vertical direction
        // This prevents insane rotation around the Y axis when looking straight up or down
        float factor;
        if (angleX > 180)
        {
            factor = Mathf.Cos((angleX - 360) / -(360 - minY - 10) * 0.5F * Mathf.PI);
        } else
        {
            factor = Mathf.Cos((angleX) / -(maxY + 10.0F) * 0.5F * Mathf.PI);
        }

        // Rotate the Follow Target transform around the X axis based on the input
        float angleChange = _look.x * SensitivityX * GeneralCameraSensitivity * (InvertCameraX ? -1.0F : 1.0F) * factor;
        CameraFollowTarget.transform.rotation *= Quaternion.AngleAxis(angleChange, Vector3.up);

        // Rotate around the Y axis
        angleChange = _look.y * SensitivityY * GeneralCameraSensitivity * (InvertCameraY ? 1.0F : -1.0F);
        CameraFollowTarget.transform.rotation *= Quaternion.AngleAxis(angleChange, Vector3.right);

        // Update angles with new rotation
        angleX = CameraFollowTarget.transform.localEulerAngles.x;
        Vector3 localEulerAngles = CameraFollowTarget.transform.localEulerAngles;
        localEulerAngles.z = 0;

        //Clamp the Up/Down rotation
        if (angleX > 180 && angleX < minY)
        {
            localEulerAngles.x = minY;
        }
        else if (angleX < 180 && angleX > maxY)
        {
            localEulerAngles.x = maxY;
        }
        
        // Rotate around the X axis
        CameraFollowTarget.transform.localEulerAngles = localEulerAngles;
    }
}
