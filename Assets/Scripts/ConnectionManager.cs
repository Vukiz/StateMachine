using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ConnectionManager
{
	private readonly PreloadStateController _stateController;

	public ConnectionManager()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_stateController.StateEntered += OnStateEntered;
	}

	private void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.Connect:
			{
				void ConnectionAction()
				{
					Debug.Log("Connecting");
					Thread.Sleep(2000);
					Connect();
				}

				Task connectionTask = new Task(ConnectionAction);
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

	private void Connect()
	{
		_stateController.Connected();
	}

	private void Disconnect()
	{
		_stateController.Disconnected();
	}
}