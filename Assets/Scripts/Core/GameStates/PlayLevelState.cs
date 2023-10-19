using System;
using System.Threading;
using Core.GameStates.LevelCompletion;
using Core.Levels;
using Core.Services;
using Cysharp.Threading.Tasks;
using GameUI;
using GameUI.Panels;
using StateMachines;
using Tools.Extensions;

namespace Core.GameStates
{
	public class PlayLevelState : PayloadState<Level>
	{
		private readonly LevelPlayer _levelPlayer;
		private readonly IUIManager _uiManager;
		
		private CancellationTokenSource _cts;
		private Level _currentLevel;

		public PlayLevelState(StateMachine stateMachine, LevelPlayer levelPlayer, IUIManager uiManager) : base(stateMachine)
		{
			_levelPlayer = levelPlayer;
			_uiManager = uiManager;
		}

		public override void SetPayload(Level level) => _currentLevel = level;

		public override void OnEnter()
		{
			_cts = CancellationTokenSource.CreateLinkedTokenSource(((GameStateMachine) StateMachine).CT);
			PlayLevel(_currentLevel, _cts.Token).Forget();
		}

		private async UniTaskVoid PlayLevel(Level level, CancellationToken ct)
		{
			if (_uiManager.TryGet(out GameHud hud))
			{
				hud.RestartClicked += OnRestartClicked;
				hud.NextClicked += OnNextClicked;
				hud.SetRestartActive(true);
			}
			await Backdrop.Raise(ct);
			
			try
			{
				await _levelPlayer.Play(level, ct);
				StateMachine.Enter<CompleteLevelState, Level>(level);
			}
			catch (OperationCanceledException) { }
		}

		private void OnRestartClicked() => StateMachine.Enter<RestartLevelState, Level>(_currentLevel);
		private void OnNextClicked() => StateMachine.Enter<CompleteLevelState, Level>(_currentLevel);

		public override void OnExit()
		{
			if (_uiManager.TryGet(out GameHud hud))
			{
				hud.RestartClicked -= OnRestartClicked;
				hud.NextClicked -= OnNextClicked;
				hud.SetRestartActive(false);
			}
			_cts.CancelAndDispose();
			_currentLevel = null;
			_cts = null;
		}
	}
}