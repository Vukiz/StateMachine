public abstract class StateMachineClient<T> : IStateMachineClient
{
    private readonly IStateMachine<T> _machine;

    protected StateMachineClient(IStateMachine<T> machine)
    {
        _machine = machine;
    }

    public virtual void OnStateEntered()
    {
    }

    public virtual void OnStateExit()
    {
    }
    
    protected void SetTrigger(T trigger)
    {
        _machine.SetTrigger(trigger);
    }
}