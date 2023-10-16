using Core.Services;
using Data.Levels;
using StateMachines;

namespace Core.GameStates.LevelsLoading
{
	public class LoadLevelState : PayloadState<int>
	{
		private readonly LevelsProvider _levelsProvider;

		public LoadLevelState(StateMachine stateMachine, LevelsProvider levelsProvider) : base(stateMachine) =>
			_levelsProvider = levelsProvider;

		public override void SetPayload(int level) => _levelsProvider.MoveTo(level);
		public override void OnEnter() => StateMachine.Enter<BuildLevelState, LevelDescription>(_levelsProvider.Current);

		public override void OnExit() { }
	}
}