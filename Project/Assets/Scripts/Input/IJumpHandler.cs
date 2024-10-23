using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IJumpHandler
{
    abstract void OnJumpInput(InputValue value);
}

