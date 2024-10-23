using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void CloseDoor()
    {
        if (_animator == null) return;
        
        _animator.SetTrigger("Close");
    }

    public void OpenDoor()
    {
        if (_animator == null) return;

        _animator.SetTrigger("Open");
    }
}
