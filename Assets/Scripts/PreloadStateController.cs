using System;
using System.Collections.Generic;
using Stateless;
using UnityEngine;

public class PreloadStateController
{
	private enum PreloadStateTrigger
	{
		Start,
		EndpointsGot,
		AssetsLoaded,
		Connected,
		Authenticated,
		Authorized,
		UserDataReceived,
		NoUserDataReceived,
		UserCreated,
		Disconnected,
		DissconnectCompleted,
	}

	private readonly StateMachine<PreloadMachineState, PreloadStateTrigger> _machine;

	public Action<PreloadMachineState> StateEntered;

	public PreloadStateController()
	{
		_machine = new StateMachine<PreloadMachineState, PreloadStateTrigger>(PreloadMachineState.GetEndpoints);
		_machine.SetTriggerParameters<List<Endpoint>>(PreloadStateTrigger.EndpointsGot);

		ConfigureTransitions();

		ConfigureOnEntry();
	}

	private void ConfigureTransitions()
	{
		_machine.Configure(PreloadMachineState.Start)
			.Permit(PreloadStateTrigger.Start, PreloadMachineState.GetEndpoints);

		_machine.Configure(PreloadMachineState.GetEndpoints)
			.Permit(PreloadStateTrigger.EndpointsGot, PreloadMachineState.AssetLoading);

		_machine.Configure(PreloadMachineState.AssetLoading)
			.Permit(PreloadStateTrigger.AssetsLoaded, PreloadMachineState.Connect);

		_machine.Configure(PreloadMachineState.Connect)
			.Permit(PreloadStateTrigger.Connected, PreloadMachineState.Authentication)
			.Permit(PreloadStateTrigger.Disconnected, PreloadMachineState.Disconnect);

		_machine.Configure(PreloadMachineState.Authentication)
			.Permit(PreloadStateTrigger.Authenticated, PreloadMachineState.Authorization);

		_machine.Configure(PreloadMachineState.Authorization)
			.Permit(PreloadStateTrigger.Authenticated, PreloadMachineState.GetUserData)
			.Permit(PreloadStateTrigger.Disconnected, PreloadMachineState.Disconnect);

		_machine.Configure(PreloadMachineState.Disconnect)
			.Permit(PreloadStateTrigger.Authorized, PreloadMachineState.GetUserData)
			.Permit(PreloadStateTrigger.Disconnected, PreloadMachineState.Disconnect);

		_machine.Configure(PreloadMachineState.GetUserData)
			.Permit(PreloadStateTrigger.UserDataReceived, PreloadMachineState.Game)
			.Permit(PreloadStateTrigger.NoUserDataReceived, PreloadMachineState.CreateNewUser);

		_machine.Configure(PreloadMachineState.CreateNewUser)
			.Permit(PreloadStateTrigger.UserCreated, PreloadMachineState.Game);

		_machine.Configure(PreloadMachineState.Game)
			.Permit(PreloadStateTrigger.Disconnected, PreloadMachineState.Disconnect);

		_machine.Configure(PreloadMachineState.Disconnect)
			.Permit(PreloadStateTrigger.DissconnectCompleted, PreloadMachineState.WaitUserInput);
	}

	private void ConfigureOnEntry()
	{
		_machine.Configure(PreloadMachineState.GetEndpoints).OnEntry(() => InvokeStateEntered(PreloadMachineState.GetEndpoints));
		_machine.Configure(PreloadMachineState.AssetLoading).OnEntry(() => InvokeStateEntered(PreloadMachineState.AssetLoading));
		_machine.Configure(PreloadMachineState.Connect).OnEntry(() => InvokeStateEntered(PreloadMachineState.Connect));
		_machine.Configure(PreloadMachineState.Disconnect).OnEntry(() => InvokeStateEntered(PreloadMachineState.Disconnect));
		_machine.Configure(PreloadMachineState.Authentication).OnEntry(() => InvokeStateEntered(PreloadMachineState.Authentication));
		_machine.Configure(PreloadMachineState.Authorization).OnEntry(() => InvokeStateEntered(PreloadMachineState.Authorization));
		_machine.Configure(PreloadMachineState.GetUserData).OnEntry(() => InvokeStateEntered(PreloadMachineState.GetUserData));
		_machine.Configure(PreloadMachineState.CreateNewUser).OnEntry(() => InvokeStateEntered(PreloadMachineState.CreateNewUser));
		_machine.Configure(PreloadMachineState.Game).OnEntry(() => InvokeStateEntered(PreloadMachineState.Game));
		_machine.Configure(PreloadMachineState.WaitUserInput).OnEntry(() => InvokeStateEntered(PreloadMachineState.WaitUserInput));
	}

	private void InvokeStateEntered(PreloadMachineState state)
	{
		Debug.Log("[StateEnter] " + state);
		StateEntered?.Invoke(state);
	}

	public void Start()
	{
		_machine.Fire(PreloadStateTrigger.Start);
	}

	public void Disconnected()
	{
		Debug.Log("[TriggerFire] Disconnect");
		_machine.Fire(PreloadStateTrigger.Disconnected);
	}
	
	public void Connected()
	{		
		Debug.Log("[TriggerFire] Connected");
		_machine.Fire(PreloadStateTrigger.Connected);
	}

	public void DissconnectCompleted()
	{
		Debug.Log("[TriggerFire] DissconnectCompleted");
		_machine.Fire(PreloadStateTrigger.DissconnectCompleted);
	}

	public void AssetsLoaded()
	{
		Debug.Log("[TriggerFire] AssetsLoaded");
		_machine.Fire(PreloadStateTrigger.AssetsLoaded);
	}

	public void Authenticated()
	{
		Debug.Log("[TriggerFire] Authenticated");
		_machine.Fire(PreloadStateTrigger.Authenticated);
	}

	public void Authorized()
	{
		Debug.Log("[TriggerFire] Authorized");
		_machine.Fire(PreloadStateTrigger.Authorized);
	}

	public void UserDataReceived()
	{
		Debug.Log("[TriggerFire] UserDataReceived");
		_machine.Fire(PreloadStateTrigger.UserDataReceived);
	}

	public void UserCreated()
	{
		Debug.Log("[TriggerFire] UserCreated");
		_machine.Fire(PreloadStateTrigger.UserCreated);
	}

	public void NoUserDataReceived()
	{
		Debug.Log("[TriggerFire] NoUserDataReceived");
		_machine.Fire(PreloadStateTrigger.NoUserDataReceived);
	}
}