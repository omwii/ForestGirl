using System;
using System.Collections.Generic;

public class FSM
{
    public FSM_State CurrentState { get; set; }

    private Dictionary<Type, FSM_State> _states = new Dictionary<Type, FSM_State>();

    public void AddState(FSM_State state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FSM_State
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
        {
            return;
        }

        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();
        }
    }
}