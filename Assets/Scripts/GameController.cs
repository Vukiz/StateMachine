using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private PreloadStateController _stateController;
	private void Start()
	{
		_stateController = new PreloadStateController();
		_stateController.OnEntrySubscribe(PreloadMachineState.AssetLoading, OnAssetLoadingEntry);
	}

	private void OnAssetLoadingEntry()
	{
		Debug.Log("Assets start Loading");
		//AssetsLoader.Load());
	}
}