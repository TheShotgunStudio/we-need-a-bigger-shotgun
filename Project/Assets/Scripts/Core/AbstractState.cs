using System;

/// <summary>
/// An abstract state for use in a state machine
/// </summary>
public interface AbstractState : ICloneable
{
    /// <summary>
    /// Called when the FSM changes to this state.
    /// </summary>
    public void OnStateEnter() { }

    /// <summary>
    /// Called when some processing, such as a per frame update, is applied to the FSM.
    /// </summary>
    public void OnStateProcessing() { }

    /// <summary>
    /// Called per fixed update if this state is the current state of the FSM.
    /// </summary>
    public void OnStateFixedProcessing() { }

    /// <summary>
    /// Called when the FSM changes away from this state.
    /// </summary>
    public void OnStateExit() { }
}
