using Core.Entities;
using Core.Entities.Views;
using Data.Levels.Visuals;

namespace Core.Levels
{
	public class VisualsService
	{
		public void ApplyVisuals(VisualSettings visuals, Entity[] entities)
		{
			foreach (Entity entity in entities)
			{
				if (!entity.TryResolve(out EntityView view)) continue;
				view.SetVisuals(visuals);
			}
		}
	}
}