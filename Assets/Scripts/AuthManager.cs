using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AuthManager
{
	private readonly PreloadStateController _stateController;

	public AuthManager()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_stateController.StateEntered += OnStateEntered;
	}

	private void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.Authentication:
			{
				void Authenticate()
				{
					Debug.Log("Authentication");
					Thread.Sleep(1000);
					Authenticated();
				}

				var authenticateTask = new Task(Authenticate);
				authenticateTask.Start();
				break;
			}
			case PreloadMachineState.Authorization:
			{
				void Authorize()
				{
					Debug.Log("Authorization");
					Thread.Sleep(1000);
					Authorized();
				}

				var authorizeTask = new Task(Authorize);
				authorizeTask.Start();
				break;
			}
		}
	}

	private void Authenticated()
	{
		_stateController.Trigger(PreloadStateTrigger.Authenticated);
	}
	
	private void Authorized()
	{
		_stateController.Trigger(PreloadStateTrigger.Authorized);
	}
}