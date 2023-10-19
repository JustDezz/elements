using Core.GameStates.LevelsLoading;
using GameInput;
using GameUI;
using GameUI.Panels;
using StateMachines;
using Zenject;

namespace Core.GameStates
{
	public class BootstrapState : State
	{
		private readonly DiContainer _container;

		public BootstrapState(StateMachine stateMachine, DiContainer container) : base(stateMachine) => _container = container;

		public override void OnEnter()
		{
			if (_container.Resolve<IInput>() is InputBase input) input.InputLoop(((GameStateMachine) StateMachine).CT).Forget();
			_container.Resolve<IUIFactory>().SpawnRoot();

			Backdrop.Drop(0);
			StateMachine.Enter<LoadCurrentLevelState>();
		}

		public override void OnExit()
		{
		}
	}
}