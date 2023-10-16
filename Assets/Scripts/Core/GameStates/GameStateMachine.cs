using System;
using System.Collections.Generic;
using System.Threading;
using Core.GameStates.LevelsLoading;
using StateMachines;
using Zenject;

namespace Core.GameStates
{
	public class GameStateMachine : StateMachine
	{
		private readonly CancellationTokenSource _cts;

		// ReSharper disable once InconsistentNaming
		public CancellationToken CT => _cts.Token;

		public GameStateMachine()
		{
			States = new Dictionary<Type, IState>();
			_cts = new CancellationTokenSource();
		}

		public void Init(IInstantiator instantiator)
		{
			object[] extraArgs = {this};
			AddState<BootstrapState>(instantiator, extraArgs);
			AddState<BuildLevelState>(instantiator, extraArgs);
			AddState<PlayLevelState>(instantiator, extraArgs);
			AddState<LoadNextLevelState>(instantiator, extraArgs);
			AddState<RestartLevelState>(instantiator, extraArgs);
			AddState<LoadLevelState>(instantiator, extraArgs);
			AddState<CompleteLevelState>(instantiator, extraArgs);
		}

		private void AddState<T>(IInstantiator instantiator, object[] extraArgs) where T : State =>
			States.Add(typeof(T), instantiator.Instantiate<T>(extraArgs));
	}
}