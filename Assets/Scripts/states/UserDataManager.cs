using System.Threading;
using System.Threading.Tasks;
using Microsoft.MinIoC;
using UnityEngine;

public class UserDataManager : PreloadingStateMachineClient
{
    public UserDataManager(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }

    public override void OnStateEntered()
    {
        void GetUserData()
        {
            Debug.Log("Loading UserData");
            Thread.Sleep(1000);
            UserDataReceived("data");
        }

        var getuserDataTask = new Task(GetUserData);
        getuserDataTask.Start();
    }

    private void UserDataReceived(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            SetTrigger(PreloadStateTrigger.NoUserDataReceived);
        }
        else
        {
            SetTrigger(PreloadStateTrigger.UserDataReceived);
        }
    }
}