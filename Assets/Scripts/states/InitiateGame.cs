public class InitiateGame : PreloadingStateMachineClient
{
    public InitiateGame(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }

    public override void OnStateEntered()
    {
        SetTrigger(PreloadStateTrigger.Disconnected);
    }
}