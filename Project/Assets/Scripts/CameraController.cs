using Cinemachine;
using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]
public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public GameObject CameraFollowTarget;
    [HideInInspector]
    public CinemachineVirtualCamera VirtualCamera;

    public Vector2 Look;

    void OnLook(InputValue value)
    {
        Look = value.Get<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<CinemachineVirtualCamera>(out VirtualCamera))
        {
            throw new NullReferenceException("CameraController did not find CinemachineVirtualCamera.");
        }
        if (VirtualCamera.Follow == null)
        {
            throw new NullReferenceException("VirtualCamera does not have Follow Target set.");
        }

        CameraFollowTarget = VirtualCamera.Follow.gameObject;
    }

    public void OnUpdate()
    {
        var angle = CameraFollowTarget.transform.localEulerAngles.x;

        float maxY = 280.0F;
        float minY = 80.0F;

        //Rotate the Follow Target transform based on the input
        // Factor decreases the degree to which the input X affects the rotation, this prevents insane rotation around the Y axis
        float factor;
        if (angle > 180)
        {
            factor = Mathf.Cos((angle - 360) / -(360 - maxY - 10) * 0.5F * Mathf.PI);
            CameraFollowTarget.transform.rotation *= Quaternion.AngleAxis(Look.x * 0.8F * factor, Vector3.up);
        } else
        {
            factor = Mathf.Cos((angle) / -(minY + 10.0F) * 0.5F * Mathf.PI);
            CameraFollowTarget.transform.rotation *= Quaternion.AngleAxis(Look.x * 0.8F * factor, Vector3.up);
        }

        // Rotate around the Y axis
        CameraFollowTarget.transform.rotation *= Quaternion.AngleAxis(Look.y * 0.5F, Vector3.right);

        var angles = CameraFollowTarget.transform.localEulerAngles;
        angles.z = 0;

        angle = CameraFollowTarget.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < maxY)
        {
            angles.x = maxY;
        }
        else if (angle < 180 && angle > minY)
        {
            angles.x = minY;
        }
        
        // Rotate around the X axis
        CameraFollowTarget.transform.localEulerAngles = angles;
    }
}
