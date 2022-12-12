public class BaseState
{
    protected readonly BaseStateMachine stateMachine;

    protected BaseState(BaseStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateState() { }
    public virtual void Exit() { }
}