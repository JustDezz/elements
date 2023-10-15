using Core.Levels;
using Data.Levels;
using StateMachines;

namespace Core.GameStates
{
	public class CompleteLevelState : PayloadState<Level>
	{
		private readonly LevelCleaner _cleaner;
		private Level _level;
		private LevelsConfig _levelsConfig;

		public CompleteLevelState(StateMachine stateMachine, LevelCleaner cleaner, LevelsConfig levelsConfig) : base(stateMachine)
		{
			_levelsConfig = levelsConfig;
			_cleaner = cleaner;
		}

		public override void SetPayload(Level level) => _level = level;

		public override void OnEnter()
		{
			_cleaner.Clear(_level);
			StateMachine.Enter<BuildLevelState, LevelDescription>(_levelsConfig.Levels[0]);
		}

		public override void OnExit() => _level = null;
	}
}