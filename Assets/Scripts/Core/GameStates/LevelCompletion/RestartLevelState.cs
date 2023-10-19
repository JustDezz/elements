using Core.GameStates.LevelsLoading;
using Core.Levels;
using StateMachines;

namespace Core.GameStates.LevelCompletion
{
	public class RestartLevelState : EndLevelState
	{
		public RestartLevelState(StateMachine stateMachine, LevelCleaner cleaner) : base(stateMachine, cleaner)
		{
		}

		protected override void OnLevelCleaned() => StateMachine.Enter<LoadCurrentLevelState>();
	}
}