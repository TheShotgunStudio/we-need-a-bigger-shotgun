using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFiniteStateMachine
{
    /// <summary>
    /// A delegate that can be passed to a child ControlState to allow it to change to different states itself.
    /// </summary>
    /// <param name="state">The state to change to</param>
    public delegate void StateSetter(IAbstractState state);

    /// <summary>
    /// A dictionary containing all possible states in the FSM.
    /// </summary>
    public Dictionary<string, IAbstractState> States { get; }

    /// <summary>
    /// The current state the FSM is in.
    /// </summary>
    IAbstractState CurrentState { get; }

    /// <summary>
    /// Should handle initialization of the states dictionary and set the current state to an initial value.
    /// </summary>
    void InitializeStates();

    /// <summary>
    /// Should set the current state to the state passed as a parameter.
    /// </summary>
    /// <remarks>
    /// Optionally you can also add a check to see if the provided state is within our dictionary.
    /// </remarks>
    /// <param name="state">The state to change to.</param>
    void SetState(IAbstractState state);

    /// <summary>
    /// Should set the state by using the key that state has in the states dictionary.
    /// </summary>
    /// <param name="stateKey">The key of the state in the dictionary</param>
    void SetState(string stateKey);
}
