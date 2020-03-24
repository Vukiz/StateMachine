using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class UserDataManager
{
	private readonly PreloadStateController _stateController;

	public UserDataManager()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_stateController.StateEntered += OnStateEntered;
	}

	private void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.GetUserData:
			{
				void GetUserData()
				{
					Debug.Log("Loading UserData");
					Thread.Sleep(1000);
					UserDataReceived("data");
				}

				var getuserDataTask = new Task(GetUserData);
				getuserDataTask.Start();
				break;
			}
		}
	}

	private void UserDataReceived(string data)
	{
		if (string.IsNullOrEmpty(data))
		{
			_stateController.NoUserDataReceived();
		}
		else
		{
			_stateController.UserDataReceived();
		}
	}
}