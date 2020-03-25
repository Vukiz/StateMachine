using System.Threading;
using System.Threading.Tasks;
using Microsoft.MinIoC;
using UnityEngine;

public class AssetLoader : PreloadingStateMachineClient
{
    public override void OnStateEntered()
    {
        void LoadAssets()
        {
            Debug.Log("Loading Assets");
            Thread.Sleep(1000);
            AssetsLoaded();
        }

        var loadAssetsTask = new Task(LoadAssets);
        loadAssetsTask.Start();
    }


    private void AssetsLoaded()
    {
        SetTrigger(PreloadStateTrigger.AssetsLoaded);
    }

    public AssetLoader(IStateMachine<PreloadStateTrigger> machine) : base(machine)
    {
    }
}