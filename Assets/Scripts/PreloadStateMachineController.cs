using System;
using System.Collections.Generic;
using Stateless;
using Stateless.Graph;
using UnityEngine;

public enum PreloadStateTrigger
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
	ReconnectFailed,
	Reconnect,
}

public class PreloadStateMachineController : IStateMachine<PreloadStateTrigger>
{
	private readonly StateMachine<PreloadMachineState, PreloadStateTrigger> _machine;

	private readonly Dictionary<PreloadMachineState, IStateMachineClient> _statesMap =
		new Dictionary<PreloadMachineState, IStateMachineClient>();

	public PreloadStateMachineController()
	{
		_machine = new StateMachine<PreloadMachineState, PreloadStateTrigger>(PreloadMachineState.Start);
		ConfigureTransitions();
		ConfigureStates();
	}

	private void ConfigureTransitions()
	{
		Debug.Log($"Default state {_machine.State}");
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
			.Permit(PreloadStateTrigger.Reconnect, PreloadMachineState.Connect)
			.Permit(PreloadStateTrigger.ReconnectFailed, PreloadMachineState.WaitUserInput);

		_machine.Configure(PreloadMachineState.WaitUserInput)
			.Permit(PreloadStateTrigger.Start, PreloadMachineState.GetEndpoints);
	}

	private void ConfigureStates()
	{
		ConfigureState(PreloadMachineState.Start, new InitiateGame(this));
		ConfigureState(PreloadMachineState.GetEndpoints, new EndpointLoader(this));
		ConfigureState(PreloadMachineState.AssetLoading, new AssetLoader(this));
		ConfigureState(PreloadMachineState.Connect, new Connection(this));
		ConfigureState(PreloadMachineState.Authentication, new AuthenticationManager(this));
		ConfigureState(PreloadMachineState.Authorization, new AuthorizationManager(this));
		ConfigureState(PreloadMachineState.GetUserData, new UserDataManager(this));
		ConfigureState(PreloadMachineState.CreateNewUser, new UserCreator(this));
		ConfigureState(PreloadMachineState.Game, new StartGame(this));
		ConfigureState(PreloadMachineState.Disconnect, new Disconnection(this));
		ConfigureStateEvents(PreloadMachineState.WaitUserInput);
	}

	private void ConfigureState(PreloadMachineState state, IStateMachineClient client)
	{
		ConfigureStateEvents(state);
		_statesMap.Add(state, client);
	}

	private void ConfigureStateEvents(PreloadMachineState state)
	{
		_machine.Configure(state)
			.OnEntry(() => InvokeStateEntered(state))
			.OnExit(() => InvokeStateExit(state))
			.OnActivate(() => InvokeWithText(state, "Activate"))
			.OnDeactivate(() => InvokeWithText(state, "Deactivate"));
	}

	private void InvokeWithText(PreloadMachineState state, String text)
	{
		Debug.Log($"[State{text}] {state}");
	}

	private void InvokeStateExit(PreloadMachineState state)
	{
		Debug.Log("[StateExit] " + state);
		if (_statesMap.ContainsKey(state))
		{
			_statesMap[state].OnStateExit();
		}
	}

	private void InvokeStateEntered(PreloadMachineState state)
	{
		Debug.Log("[StateEnter] " + state);
		if (_statesMap.ContainsKey(state))
		{
			_statesMap[state].OnStateEntered();
		}
	}

	public string ToDotGraph()
	{
		return UmlDotGraph.Format(_machine.GetInfo());
	}

	public void SetTrigger(PreloadStateTrigger trigger)
	{
		Debug.Log($" >> Setting state trigger {trigger} to state {_machine.State}");
		_machine.Fire(trigger);
	}
}