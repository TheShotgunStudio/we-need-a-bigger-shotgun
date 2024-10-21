using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{   
    public Transform MainCamera;   
    public Transform PlayerModel;
    public Transform PlayerSpine;
    [SerializeField]
    private PlayerStats _baseStats;
    [HideInInspector]
    public PlayerStats Stats;

    private void Awake()
    {
        Stats = (PlayerStats)_baseStats.Clone();
    }
    
    void Update(){
        PlayerModel.transform.rotation = Quaternion.Euler(0,  MainCamera.transform.rotation.eulerAngles.y, 0);
        Quaternion lookRotation = Quaternion.LookRotation((MainCamera.position + MainCamera.forward * 10.0f) - PlayerSpine.position);
        PlayerSpine.rotation = math.slerp(PlayerSpine.rotation, lookRotation, 0.9f);
    }
}
