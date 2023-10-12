using System;
using System.Collections.Generic;
using StateMachines;
using Zenject;

namespace Core.GameStates
{
	public class GameStateMachine : StateMachine
	{
		public GameStateMachine() => States = new Dictionary<Type, IState>();

		public void Init(IInstantiator instantiator)
		{
			object[] extraArgs = {this};
			States.Add(typeof(BootstrapState), instantiator.Instantiate<BootstrapState>(extraArgs));
			States.Add(typeof(PlayLevelState), instantiator.Instantiate<PlayLevelState>(extraArgs));
		}
	}
}