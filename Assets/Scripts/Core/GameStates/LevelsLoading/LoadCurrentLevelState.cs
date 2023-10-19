using Core.Services;
using Cysharp.Threading.Tasks;
using Data.Levels;
using GameUI.Panels;
using StateMachines;

namespace Core.GameStates.LevelsLoading
{
	public class LoadCurrentLevelState : State
	{
		protected readonly LevelsProvider LevelsProvider;

		public LoadCurrentLevelState(StateMachine stateMachine, LevelsProvider levelsProvider) : base(stateMachine) =>
			LevelsProvider = levelsProvider;

		public override void OnEnter() => Animate().Forget();

		private async UniTaskVoid Animate()
		{
			await Backdrop.Drop(((GameStateMachine) StateMachine).CT);
			StateMachine.Enter<BuildLevelState, LevelDescription>(LevelsProvider.Current);
		}

		public override void OnExit() { }
	}
}