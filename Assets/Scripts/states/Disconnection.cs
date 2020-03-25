using UnityEngine;

public class Disconnection : PreloadingStateMachineClient
{
	private int ReconnectionsCount = 0;

	public Disconnection(IStateMachine<PreloadStateTrigger> machine) : base(machine)
	{
	}

	public override void OnStateEntered()
	{
		if (ReconnectionsCount++ < 3)
		{
			Debug.Log($"[Disconnected] Trying to reconnect {ReconnectionsCount}");
			SetTrigger(PreloadStateTrigger.Reconnect);
		}
		else
		{
			ReconnectionsCount = 0;
			SetTrigger(PreloadStateTrigger.ReconnectFailed);
		}
	}
}