using System;
using System.Collections.Generic;

namespace StateMachines
{
	public class StateMachine
	{
		protected Dictionary<Type, IState> States;

		private IState _currentState;

		public void Enter<TState>() where TState : IState => SetState(States[typeof(TState)]);
		public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
		{
			TState targetState = (TState) States[typeof(TState)];
			targetState.SetPayload(payload);
			SetState(targetState);
		}

		private void SetState(IState targetState)
		{
			_currentState?.OnExit();
			targetState.OnEnter();
			_currentState = targetState;
		}
	}
}
