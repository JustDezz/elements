using System.Threading;
using Core.Levels;
using Data.Levels;
using StateMachines;
using Tools.Extensions;

namespace Core.GameStates
{
	public class PlayLevelState : PayloadState<LevelDescription>
	{
		private readonly LevelFactory _levelFactory;
		
		private CancellationTokenSource _cts;
		private LevelDescription _currentLevelDescription;
		private Level _currentLevel;

		public PlayLevelState(StateMachine stateMachine, LevelFactory levelFactory) : base(stateMachine) => _levelFactory = levelFactory;

		public override void SetPayload(LevelDescription level) => _currentLevelDescription = level;

		public override void OnEnter()
		{
			_cts = new CancellationTokenSource();
			_currentLevel = _levelFactory.Create(_currentLevelDescription);
		}

		public override void OnExit() 
		{
			_levelFactory.Clear(_currentLevel);
			_cts.CancelAndDispose();
			_cts = null;
		}
	}
}