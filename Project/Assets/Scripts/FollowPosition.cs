using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPosition : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, playerTransform.transform.position + offset, 0.9f);
    }
}
