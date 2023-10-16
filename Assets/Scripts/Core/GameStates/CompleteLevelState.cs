using Core.GameStates.LevelsLoading;
using Core.Levels;
using StateMachines;

namespace Core.GameStates
{
	public class CompleteLevelState : PayloadState<Level>
	{
		private readonly LevelCleaner _cleaner;
		private Level _level;

		public CompleteLevelState(StateMachine stateMachine, LevelCleaner cleaner) : base(stateMachine) => _cleaner = cleaner;

		public override void SetPayload(Level level) => _level = level;

		public override void OnEnter()
		{
			_cleaner.Clear(_level);
			StateMachine.Enter<LoadNextLevelState>();
		}

		public override void OnExit() => _level = null;
	}
}