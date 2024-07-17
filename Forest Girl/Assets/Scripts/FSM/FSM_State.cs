public class FSM_State
{
    protected readonly FSM Fsm;

    public FSM_State(FSM fsm)
    {
        Fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
