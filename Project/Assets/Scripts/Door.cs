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
        Debug.Log("Closing: " + this.gameObject.name);
        if (_animator == null) return;
        
        _animator.SetTrigger("Close");
    }

    public void OpenDoor()
    {
        Debug.Log("Opening: " + this.gameObject.name);
        if (_animator == null) return;

        _animator.SetTrigger("Open");
    }
}
