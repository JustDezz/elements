using System;
using System.Collections.Generic;
using System.Threading;
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
			States.Add(typeof(BootstrapState), instantiator.Instantiate<BootstrapState>(extraArgs));
			States.Add(typeof(PlayLevelState), instantiator.Instantiate<PlayLevelState>(extraArgs));
		}
	}
}