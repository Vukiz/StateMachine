public abstract class PreloadingStateMachineClient : StateMachineClient<PreloadStateTrigger>
{
    protected PreloadingStateMachineClient(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }
}