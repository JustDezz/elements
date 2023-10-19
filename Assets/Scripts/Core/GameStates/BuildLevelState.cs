using CameraManagement;
using Core.Levels;
using Data.Levels;
using GameUI;
using GameUI.Panels;
using StateMachines;

namespace Core.GameStates
{
	public class BuildLevelState : PayloadState<LevelDescription>
	{
		private readonly IUIManager _uiManager;
		private readonly LevelFactory _levelFactory;
		private readonly ICameraFactory _cameraFactory;
		
		private LevelDescription _currentLevel;

		public BuildLevelState(
			StateMachine stateMachine,
			IUIManager uiManager,
			LevelFactory levelFactory,
			ICameraFactory cameraFactory) : base(stateMachine)
		{
			_uiManager = uiManager;
			_cameraFactory = cameraFactory;
			_levelFactory = levelFactory;
		}

		public override void SetPayload(LevelDescription levelDescription) => _currentLevel = levelDescription;

		public override void OnEnter()
		{
			if (_uiManager.TryGet<GameHud>() == null)
			{
				GameHud hud = _uiManager.Open<GameHud>();
				hud.SetRestartActive(false);
			}
			
			Level level = _levelFactory.Create(_currentLevel);
			SetupCamera(level);
			
			StateMachine.Enter<PlayLevelState, Level>(level);
		}

		private void SetupCamera(Level level)
		{
			GameCamera camera = _cameraFactory.GetCamera();
			camera.Frame(level.Bounds);
		}

		public override void OnExit() => _currentLevel = null;
	}
}