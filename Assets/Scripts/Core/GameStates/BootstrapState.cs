using Data.Levels;
using GameInput;
using StateMachines;
using Zenject;

namespace Core.GameStates
{
	public class BootstrapState : State
	{
		private readonly DiContainer _container;
		private readonly LevelsConfig _levelsConfig;

		public BootstrapState(StateMachine stateMachine, DiContainer container) : base(stateMachine)
		{
			_container = container;
			_levelsConfig = _container.Resolve<LevelsConfig>();
		}

		public override void OnEnter()
		{
			if (_container.Resolve<IInput>() is InputBase input) input.InputLoop(((GameStateMachine) StateMachine).CT).Forget();
			StateMachine.Enter<PlayLevelState, LevelDescription>(_levelsConfig.Levels[0]);
		}

		public override void OnExit()
		{
		}
	}
}