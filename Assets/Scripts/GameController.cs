using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _printGraphButton;

	private EndpointLoader _endpointLoader;
	private AssetLoader _assetLoader;
	private ConnectionManager _connectionManager;
	private AuthManager _authManager;
	private UserDataManager _userDataManager;
	private UserCreator _userCreator;
	
	
	private PreloadStateController _stateController;

	private void Awake()
	{
		_stateController = IoCContainer.Resolve<PreloadStateController>();
		_endpointLoader =  IoCContainer.Resolve<EndpointLoader>();
		_assetLoader =  IoCContainer.Resolve<AssetLoader>();
		_connectionManager = IoCContainer.Resolve<ConnectionManager>();
		_authManager = IoCContainer.Resolve<AuthManager>();
		_userDataManager = IoCContainer.Resolve<UserDataManager>();
		_userCreator = IoCContainer.Resolve<UserCreator>();
	}

	private void Start()
	{
		_startButton.onClick.AddListener(OnStartButtonClick);
		_printGraphButton.onClick.AddListener(OnPrintButtonClick);
	}

	private void OnPrintButtonClick()
	{
		Debug.Log(_stateController.ToDotGraph());
	}

	private void OnStartButtonClick()
	{
		_stateController.Start();
	}
}