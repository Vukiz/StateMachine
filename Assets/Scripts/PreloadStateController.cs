using System;
using System.Collections.Generic;
using Stateless;

namespace DefaultNamespace
{
	public class PreloadStateController
	{

		private readonly StateMachine<PreloadMachineState, PreloadStateTrigger> _machine;
		
		private StateMachine<PreloadMachineState, PreloadStateTrigger>.TriggerWithParameters<List<Endpoint>> _assignTrigger;

		public PreloadStateController()
		{
			_machine = new StateMachine<PreloadMachineState, PreloadStateTrigger>(PreloadMachineState.GetEndpoints);
			_machine.SetTriggerParameters<List<Endpoint>>(PreloadStateTrigger.EndpointsGot);
			
			_machine.Configure(PreloadMachineState.Start)
				.Permit(PreloadStateTrigger.Start, PreloadMachineState.GetEndpoints);
			
			_machine.Configure(PreloadMachineState.GetEndpoints)
				.Permit(PreloadStateTrigger.EndpointsGot, PreloadMachineState.AssetLoading);
			
			_machine.Configure(PreloadMachineState.AssetLoading)
				.Permit(PreloadStateTrigger.AssetsLoaded, PreloadMachineState.Connect);

			_machine.Configure(PreloadMachineState.Connect)
				.Permit(PreloadStateTrigger.Connected, PreloadMachineState.Authentication)
				.Permit(PreloadStateTrigger.Dissconnected, PreloadMachineState.Disconnect);
			
			_machine.Configure(PreloadMachineState.Authentication)
				.Permit(PreloadStateTrigger.Authenticated, PreloadMachineState.Authorization);
			
			_machine.Configure(PreloadMachineState.Authorization)
				.Permit(PreloadStateTrigger.Authenticated, PreloadMachineState.GetUserData)
				.Permit(PreloadStateTrigger.Dissconnected, PreloadMachineState.Disconnect);
			
			_machine.Configure(PreloadMachineState.Disconnect)
				.Permit(PreloadStateTrigger.Authorized, PreloadMachineState.GetUserData)
				.Permit(PreloadStateTrigger.Dissconnected, PreloadMachineState.Disconnect);
			
			_machine.Configure(PreloadMachineState.GetUserData)
				.Permit(PreloadStateTrigger.UserDataReceived, PreloadMachineState.Game)
				.Permit(PreloadStateTrigger.NoUserDataReceived, PreloadMachineState.CreateNewUser);

			_machine.Configure(PreloadMachineState.CreateNewUser)
				.Permit(PreloadStateTrigger.UserCreated, PreloadMachineState.Game);

			_machine.Configure(PreloadMachineState.Game)
				.Permit(PreloadStateTrigger.Dissconnected, PreloadMachineState.Disconnect);

			_machine.Configure(PreloadMachineState.Disconnect)
				.Permit(PreloadStateTrigger.DissconnectCompleted, PreloadMachineState.WaitUserInput);
			
			
		}

		public void Disconnected()
		{
			_machine.Fire(PreloadStateTrigger.Dissconnected);
		}

		public void OnEntrySubscribe(PreloadMachineState preloadMachineState, Action onEntryAction)
		{
			_machine.Configure(preloadMachineState).OnEntry(onEntryAction);
		}
		
		public void OnExitSubscribe(PreloadMachineState preloadMachineState, Action onExitAction)
		{
			_machine.Configure(preloadMachineState).OnExit(onExitAction);
		}
	}
}