using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EndpointLoader
{
	private readonly PreloadStateController _stateController;

	public EndpointLoader()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_stateController.StateEntered += OnStateEntered;
	}

	private void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.GetEndpoints:
			{
				void LoadEndpoints()
				{
					Debug.Log("Loading Endpoints");
					Thread.Sleep(1000);
					EndpointsGot();
				}

				Task loadEndpoints = new Task(LoadEndpoints);
				loadEndpoints.Start();
				break;
			}
		}
	}

	private void EndpointsGot()
	{
		_stateController.EndpointsGot();
	}
}