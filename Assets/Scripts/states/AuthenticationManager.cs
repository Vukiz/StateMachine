using System.Threading;
using System.Threading.Tasks;
using Microsoft.MinIoC;
using UnityEngine;

public class AuthenticationManager : PreloadingStateMachineClient
{
    public AuthenticationManager(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }
    
    public override void OnStateEntered()
    {
        void Authenticate()
        {
            Debug.Log("Authentication");
            Thread.Sleep(1000);
            Authenticated();
        }

        var authenticateTask = new Task(Authenticate);
        authenticateTask.Start();
    }

    private void Authenticated()
    {
        SetTrigger(PreloadStateTrigger.Authenticated);
    }

    private void Authorized()
    {
        SetTrigger(PreloadStateTrigger.Authorized);
    }

    
}