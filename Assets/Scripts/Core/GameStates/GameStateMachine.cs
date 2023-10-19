using System;
using System.Collections.Generic;
using System.Threading;
using Core.GameStates.LevelCompletion;
using Core.GameStates.LevelsLoading;
using Cysharp.Threading.Tasks;
using StateMachines;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.GameStates
{
	public class GameStateMachine : StateMachine
	{
		// ReSharper disable once InconsistentNaming
		public CancellationToken CT { get; }

		public GameStateMachine()
		{
			States = new Dictionary<Type, IState>();
			
			GameObject cts = new("Game Cancellation Token Source");
			Object.DontDestroyOnLoad(cts);
			cts.hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector;
			CT = cts.GetCancellationTokenOnDestroy();
		}

		public void Init(IInstantiator instantiator)
		{
			object[] extraArgs = {this};
			AddState<BootstrapState>(instantiator, extraArgs);

			AddState<LoadCurrentLevelState>(instantiator, extraArgs);
			AddState<LoadNextLevelState>(instantiator, extraArgs);
			AddState<LoadLevelByIndexState>(instantiator, extraArgs);
			
			AddState<BuildLevelState>(instantiator, extraArgs);
			AddState<PlayLevelState>(instantiator, extraArgs);
			AddState<CompleteLevelState>(instantiator, extraArgs);
			AddState<RestartLevelState>(instantiator, extraArgs);
		}

		private void AddState<T>(IInstantiator instantiator, object[] extraArgs) where T : State =>
			States.Add(typeof(T), instantiator.Instantiate<T>(extraArgs));
	}
}