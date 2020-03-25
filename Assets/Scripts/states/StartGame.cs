using UnityEngine;

public class StartGame : PreloadingStateMachineClient
{
    public StartGame(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }
    public override void OnStateEntered()
    {
        Debug.Log("================== Game Started==================");
        //SetTrigger(PreloadStateTrigger.);
    }
}