using System.Threading;
using System.Threading.Tasks;
using Microsoft.MinIoC;
using UnityEngine;

public class UserCreator : PreloadingStateMachineClient
{
    public UserCreator(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }    
    public override void OnStateEntered()
    {
        void CreateUser()
        {
            Debug.Log("Loading Assets");
            Thread.Sleep(1000);
            CreatedUser();
        }

        var createUserTask = new Task(CreateUser);
        createUserTask.Start();
    }

    private void CreatedUser()
    {
        SetTrigger(PreloadStateTrigger.UserCreated);
    }
}