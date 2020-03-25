using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Connection : PreloadingStateMachineClient
{  

    public Connection(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }


    public override void OnStateEntered()
    {
        void ConnectionAction()
        {
            Debug.Log("Connecting : " + _connectionsCounter);
            Thread.Sleep(2000);
            Connect();
        }

        Task connectionTask = new Task(ConnectionAction);
        connectionTask.Start();
    }


    private void Connect()
    {
        if (TryConnect())
        {
            Debug.Log("[Connection] Ok");
            SetTrigger(PreloadStateTrigger.Connected);
        }
        else
        {
            Debug.LogWarning("[Connection] Emulate failed connection");
            SetTrigger(PreloadStateTrigger.Disconnected);
        }
    }

    private int _connectionsCounter;     
    private bool TryConnect()
    {
        if (_connectionsCounter++ < 2) // counter is here just to simulate disconnection, further in development connection shouldn't count connectionTries
        {
            return false;
        }

        _connectionsCounter = 0;
        return true;
    }
}