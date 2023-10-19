using Core.Services;
using StateMachines;

namespace Core.GameStates.LevelsLoading
{
	public class LoadNextLevelState : LoadCurrentLevelState
	{
		public LoadNextLevelState(StateMachine stateMachine, LevelsProvider levelsProvider) : base(stateMachine, levelsProvider) {}

		public override void OnEnter()
		{
			LevelsProvider.MoveNext();
			base.OnEnter();
		}
	}
}