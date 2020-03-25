using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Connection : PreloadingStateMachineClient
{
    private const int MaxConnections = 3;

    private static int connectionsCounter = 0;       

    public Connection(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }


    public override void OnStateEntered()
    {
        void ConnectionAction()
        {
            Debug.Log("Connecting : " + connectionsCounter);
            Thread.Sleep(2000);
            Connect();
        }

        Task connectionTask = new Task(ConnectionAction);
        connectionTask.Start();
    }


    private void Connect()
    {
        if (connectionsCounter++ < MaxConnections)
        {
            Debug.LogWarning("[Connection] Emulate failed connection");
            SetTrigger(PreloadStateTrigger.Reconnect);
            return;
        }
        /*
        else  //TODO disconnect logic here
        {
            connectionsCounter = 0;
            Debug.Log("[Connection] Tries timeout");
            SetTrigger(PreloadStateTrigger.Disconnected);
        }*/
        
        connectionsCounter = 0;
        Debug.Log("[Connection] Ok");
        SetTrigger(PreloadStateTrigger.Connected);
    }
}