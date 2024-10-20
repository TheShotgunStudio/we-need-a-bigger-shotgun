using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract state interface for use in a state machine that allows this state to change states by itself
/// </summary>
public interface ControlState : AbstractState
{
    /// <summary>
    /// A list of neighboring states in the FSM this state is allowed to change to
    /// </summary>
    public Dictionary<string, AbstractState> Neighbors { get; }

    /// <summary>
    /// A delegate given to this object to allow it to change its parent state machine to a given state
    /// </summary>
    public FiniteStateMachine.StateSetter StateSetter { get; }
}
