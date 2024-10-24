using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IAttackHandler
{
    abstract void OnAttackInput(InputValue value);
}
