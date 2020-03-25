using System.Threading;
using System.Threading.Tasks;
using Microsoft.MinIoC;
using UnityEngine;

public class AuthorizationManager : PreloadingStateMachineClient
{
    public AuthorizationManager(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }
    
    public override void OnStateEntered()
    {
        void Authorize()
        {
            Debug.Log("Authorization");
            Thread.Sleep(1000);
            Authorized();
        }

        var authorizeTask = new Task(Authorize);
        authorizeTask.Start();
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