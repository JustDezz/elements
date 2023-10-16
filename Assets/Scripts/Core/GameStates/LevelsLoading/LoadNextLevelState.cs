using Core.Services;
using Data.Levels;
using StateMachines;

namespace Core.GameStates.LevelsLoading
{
	public class LoadNextLevelState : State
	{
		private readonly LevelsProvider _levelsProvider;

		public LoadNextLevelState(StateMachine stateMachine, LevelsProvider levelsProvider) : base(stateMachine) =>
			_levelsProvider = levelsProvider;

		public override void OnEnter()
		{
			_levelsProvider.MoveNext();
			StateMachine.Enter<BuildLevelState, LevelDescription>(_levelsProvider.Current);
		}

		public override void OnExit() { }
	}
}