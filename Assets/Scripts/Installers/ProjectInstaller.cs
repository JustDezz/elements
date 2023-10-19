using CameraManagement;
using Core.Entities;
using Core.GameStates;
using Core.Levels;
using Core.Services;
using Data.Entities;
using Data.Levels;
using GameInput;
using GameUI;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class ProjectInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] private EntitiesConfig _entitiesConfig;
		[SerializeField] private LevelsConfig _levelsConfig;
		[SerializeField] private GridConfig _gridConfig;
		[SerializeField] private UIConfig _uiConfig;
		[SerializeField] private CameraConfig _cameraConfig;

		public void Initialize()
		{
			Container.Unbind<IInitializable>();
			GameStateMachine stateMachine = Container.Resolve<GameStateMachine>();
			stateMachine.Init(Container.Resolve<IInstantiator>());
			stateMachine.Enter<BootstrapState>();
		}

		public override void InstallBindings()
		{
			BindInstance(_entitiesConfig);
			BindInstance(_levelsConfig);
			BindInstance(_gridConfig);
			BindInstance(_uiConfig);

			BindInput();

			Container.BindInterfacesTo<UIFactory>().AsSingle();
			Container.BindInterfacesTo<UIManager>().AsSingle();
			Container.Bind<LevelsProvider>().AsSingle();
			Container.Bind<LevelFactory>().AsSingle();
			Container.Bind<EntitiesFactory>().AsSingle();
			Container.Bind<VisualsService>().AsSingle();
			Container.Bind<LevelCleaner>().AsSingle();
			Container.Bind<LevelPlayer>().AsSingle();
			Container.Bind<EntityConfigsProvider>().AsSingle();
			
			Container.BindInterfacesTo<CameraFactory>()
				.FromInstance(new CameraFactory(_cameraConfig))
				.AsSingle();

			Container.Bind<GameStateMachine>().AsSingle();
			
			Container.Bind<IInitializable>().FromInstance(this).AsSingle();
		}

		private void BindInput()
		{
#if UNITY_EDITOR
			Container.BindInterfacesTo<PCInput>().AsSingle();
#else
			Container.BindInterfacesTo<MobileInput>().AsSingle();
#endif
		}

		private void BindInstance<T>(T instance) => Container.Bind<T>().FromInstance(instance);
	}
}