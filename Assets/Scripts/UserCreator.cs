using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class UserCreator
{
	private readonly PreloadStateController _stateController;

	public UserCreator()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_stateController.StateEntered += OnStateEntered;
	}

	private void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.CreateNewUser:
			{
				void CreateUser()
				{
					Debug.Log("Loading Assets");
					Thread.Sleep(1000);
					CreatedUser();
				}

				var createUserTask = new Task(CreateUser);
				createUserTask.Start();
				break;
			}
		}
	}

	private void CreatedUser()
	{
		_stateController.Trigger(PreloadStateTrigger.UserCreated);
	}
}