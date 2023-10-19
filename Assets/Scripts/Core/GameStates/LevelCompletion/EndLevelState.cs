using System.Threading;
using Core.Levels;
using Cysharp.Threading.Tasks;
using GameUI.Panels;
using StateMachines;

namespace Core.GameStates.LevelCompletion
{
	public abstract class EndLevelState : PayloadState<Level>
	{
		private readonly LevelCleaner _cleaner;

		protected Level Level;

		public EndLevelState(StateMachine stateMachine, LevelCleaner cleaner) : base(stateMachine) => _cleaner = cleaner;

		public override void SetPayload(Level level) => Level = level;
		
		public override void OnEnter() => Animate(((GameStateMachine) StateMachine).CT).Forget();

		private async UniTaskVoid Animate(CancellationToken ct)
		{
			OnBeforeBackdrop();
			await Backdrop.Drop(ct);
			OnBackdropDropped();
			
			_cleaner.Clear(Level);
			OnLevelCleaned();
		}

		protected virtual void OnBeforeBackdrop() { }
		protected virtual void OnBackdropDropped() { }
		protected abstract void OnLevelCleaned(); 

		public override void OnExit() => Level = null;
	}
}