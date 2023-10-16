using Core.Services;
using Data.Levels;
using StateMachines;

namespace Core.GameStates.LevelsLoading
{
	public class RestartLevelState : State
	{
		private readonly LevelsProvider _levelsProvider;

		public RestartLevelState(StateMachine stateMachine, LevelsProvider levelsProvider) : base(stateMachine) =>
			_levelsProvider = levelsProvider;

		public override void OnEnter() => StateMachine.Enter<BuildLevelState, LevelDescription>(_levelsProvider.Current);
		public override void OnExit() { }
	}
}