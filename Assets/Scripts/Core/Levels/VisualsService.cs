using Core.Entities;
using Core.Entities.Views;
using Core.Environment;
using Data.Levels.Visuals;
using Zenject;

namespace Core.Levels
{
	public class VisualsService
	{
		private readonly IInstantiator _instantiator;
		
		public LevelEnvironment CurrentEnvironment { get; private set; }

		public VisualsService(IInstantiator instantiator) => _instantiator = instantiator;

		public void ApplyVisuals(VisualSettings visuals, Level level)
		{
			CurrentEnvironment = _instantiator.InstantiatePrefabForComponent<LevelEnvironment>(visuals.Environment, level.Root);
			
			foreach (Entity entity in level.Entities)
			{
				if (!entity.TryResolve(out EntityView view)) continue;
				view.SetVisuals(visuals);
			}
			
			CurrentEnvironment.Init(level);
		}
	}
}