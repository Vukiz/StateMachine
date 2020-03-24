using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public static class ConnectionManager
{
	private static readonly PreloadStateController StateController;

	static ConnectionManager()
	{
		StateController = IoCContainer.Resolve<PreloadStateController>();
		StateController.StateEntered += OnStateEntered;
	}

	private static void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.Connect:
			{
				Action connectionAction = () =>
				{
					Debug.Log("Connecting");
					Thread.Sleep(2000);
					Connect(); 
				};
				Task connectionTask = new Task(Connect);
				connectionTask.Start();
				break;
			}
			case PreloadMachineState.Disconnect:
			{
				Disconnect();
				break;
			}
		}
	}

	private static void Connect()
	{
		StateController.Connected();
	}

	private static void Disconnect()
	{
		StateController.Disconnected();
	}
}