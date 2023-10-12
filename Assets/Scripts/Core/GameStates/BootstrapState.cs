using Data.Levels;
using StateMachines;

namespace Core.GameStates
{
	public class BootstrapState : State
	{
		private LevelsConfig _levelsConfig;

		public BootstrapState(StateMachine stateMachine, LevelsConfig levelsConfig) : base(stateMachine) => _levelsConfig = levelsConfig;

		public override void OnEnter()
		{
			StateMachine.Enter<PlayLevelState, LevelDescription>(_levelsConfig.Levels[0]);
		}

		public override void OnExit()
		{
		}
	}
}