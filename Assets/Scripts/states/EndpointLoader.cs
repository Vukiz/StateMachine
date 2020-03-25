using System.Threading;
using System.Threading.Tasks;
using Microsoft.MinIoC;
using UnityEngine;

public class EndpointLoader : PreloadingStateMachineClient
{
    public EndpointLoader(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }
    public override void OnStateEntered()
    {
        void LoadEndpoints()
        {
            Debug.Log("Loading Endpoints");
            Thread.Sleep(1000);
            EndpointsGot();
        }

        var loadEndpoints = new Task(LoadEndpoints);
        loadEndpoints.Start();
    }

    private void EndpointsGot()
    {
        SetTrigger(PreloadStateTrigger.EndpointsGot);
    }

  
}