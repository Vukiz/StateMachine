using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AssetLoader
{
	private readonly PreloadStateController _stateController;

	public AssetLoader()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_stateController.StateEntered += OnStateEntered;
	}

	private void OnStateEntered(PreloadMachineState newState)
	{
		switch (newState)
		{
			case PreloadMachineState.AssetLoading:
			{
				void LoadAssets()
				{
					Debug.Log("Loading Assets");
					Thread.Sleep(1000);
					AssetsLoaded();
				}

				var loadAssetsTask = new Task(LoadAssets);
				loadAssetsTask.Start();
				break;
			}
		}
	}

	private void AssetsLoaded()
	{
		_stateController.Trigger(PreloadStateTrigger.AssetsLoaded);
	}
}