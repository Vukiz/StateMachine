using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _printGraphButton;

    private EndpointLoader _endpointLoader;
    private AssetLoader _assetLoader;
    private Connection _connection;
    private AuthenticationManager _authenticationManager;
    private UserDataManager _userDataManager;
    private UserCreator _userCreator;

    private PreloadStateMachineController _stateMachineController;

    private void Awake()
    {
        _stateMachineController = IoCContainer.Resolve<PreloadStateMachineController>();
    }

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _printGraphButton.onClick.AddListener(OnPrintButtonClick);
    }

    private void OnPrintButtonClick()
    {
        Debug.Log(_stateMachineController.ToDotGraph());
    }

    private void OnStartButtonClick()
    {
        _stateMachineController.SetTrigger(PreloadStateTrigger.Start);
    }
}