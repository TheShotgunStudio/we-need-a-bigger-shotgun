using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CloseDoor()
    {
        if (animator == null) return;
        
        animator.SetTrigger("Close");
    }

    public void OpenDoor()
    {
        if (animator == null) return;

        animator.SetTrigger("Open");
    }
}
