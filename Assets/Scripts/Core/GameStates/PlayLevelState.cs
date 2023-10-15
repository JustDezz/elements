using System.Threading;
using Core.Levels;
using Core.Services;
using Cysharp.Threading.Tasks;
using StateMachines;
using Tools.Extensions;

namespace Core.GameStates
{
	public class PlayLevelState : PayloadState<Level>
	{
		private readonly LevelPlayer _levelPlayer;
		
		private CancellationTokenSource _cts;
		private Level _currentLevel;

		public PlayLevelState(StateMachine stateMachine, LevelPlayer levelPlayer) : base(stateMachine) => _levelPlayer = levelPlayer;

		public override void SetPayload(Level level) => _currentLevel = level;

		public override void OnEnter()
		{
			_cts = CancellationTokenSource.CreateLinkedTokenSource(((GameStateMachine) StateMachine).CT);
			PlayLevel(_currentLevel, _cts.Token).Forget();
		}

		private async UniTaskVoid PlayLevel(Level level, CancellationToken ct)
		{
			await _levelPlayer.Play(level, ct);
			StateMachine.Enter<CompleteLevelState, Level>(level);
		}

		public override void OnExit() 
		{
			_cts.CancelAndDispose();
			_currentLevel = null;
			_cts = null;
		}
	}
}