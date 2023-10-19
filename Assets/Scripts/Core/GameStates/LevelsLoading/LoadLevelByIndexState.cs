using Core.Services;
using StateMachines;

namespace Core.GameStates.LevelsLoading
{
	public class LoadLevelByIndexState : LoadCurrentLevelState, IPayloadState<int>
	{
		public LoadLevelByIndexState(StateMachine stateMachine, LevelsProvider levelsProvider) : base(stateMachine, levelsProvider) {}
		public void SetPayload(int level) => LevelsProvider.MoveTo(level);
	}
}