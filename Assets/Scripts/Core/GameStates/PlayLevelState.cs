using System.Threading;
using CameraManagement;
using Core.Levels;
using Data.Levels;
using StateMachines;
using Tools.Extensions;
using UnityEngine;

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
			GameCamera camera = Object.FindObjectOfType<GameCamera>();
			CellGrid grid = _currentLevel.Grid;
			Vector3 min = grid.ToWorld(new Vector2Int(-1, 0), Vector2.zero);
			Vector3 max = grid.ToWorld(_currentLevel.Description.Size + new Vector2Int(0, -1), Vector2.one);
			Bounds levelBounds = new();
			levelBounds.SetMinMax(min, max);
			camera.Frame(levelBounds);
		}

		public override void OnExit() 
		{
			_levelFactory.Clear(_currentLevel);
			_cts.CancelAndDispose();
			_cts = null;
		}
	}
}