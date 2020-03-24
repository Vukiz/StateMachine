using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	
	private PreloadStateController _stateController;
	private void Start()
	{
		_stateController = new PreloadStateController();
		_startButton.onClick.AddListener(OnStartButtonClick);
	}


	private void OnGetEndpointsEntry()
	{
		Debug.Log("Getting endpoints");
	}

	private void OnStartButtonClick()
	{
		_stateController.Start();
	}

	private void OnAssetLoadingEntry()
	{
		Debug.Log("Assets start Loading");
		//AssetsLoader.Load());
		//wait
		_stateController.AssetsLoaded();
	}
	
	private void OnAuthenticationEntry()
	{
		Debug.Log("Authentication");
	}

	private void OnAuthorizationEntry()
	{
		Debug.Log("Authorization");
	}

	private void OnGetUserDataEntry()
	{
		Debug.Log("GetUserData");
	}

	private void OnCreateNewUserEntry()
	{
		Debug.Log("CreateNewUser");
	}

	private void OnGameEntry()
	{
		Debug.Log("Game");
	}
	private void OnWaitUserInputEntry()
	{
		Debug.Log("WaitUserInput");
	}
}