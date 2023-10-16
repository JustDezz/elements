using CameraManagement;
using Core.Levels;
using Data.Levels;
using StateMachines;
using UnityEngine;

namespace Core.GameStates
{
	public class BuildLevelState : PayloadState<LevelDescription>
	{
		private readonly LevelFactory _levelFactory;
		private readonly ICameraFactory _cameraFactory;
		
		private LevelDescription _currentLevel;

		public BuildLevelState(
			StateMachine stateMachine,
			LevelFactory levelFactory,
			ICameraFactory cameraFactory) : base(stateMachine)
		{
			_cameraFactory = cameraFactory;
			_levelFactory = levelFactory;
		}

		public override void SetPayload(LevelDescription levelDescription) => _currentLevel = levelDescription;

		public override void OnEnter()
		{
			Level level = _levelFactory.Create(_currentLevel);
			SetupCamera(level);
			
			StateMachine.Enter<PlayLevelState, Level>(level);
		}

		private void SetupCamera(Level level)
		{
			CellGrid grid = level.Grid;
			GameCamera camera = _cameraFactory.GetCamera();
			Vector3 min = grid.ToWorld(Vector2Int.zero, Vector2.zero);
			Vector3 max = grid.ToWorld(level.Size, Vector2.zero);
			Bounds levelBounds = new();
			levelBounds.SetMinMax(min, max);
			camera.Frame(levelBounds);
		}

		public override void OnExit() => _currentLevel = null;
	}
}