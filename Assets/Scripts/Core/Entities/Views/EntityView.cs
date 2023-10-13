using Data.Levels.Visuals;
using Tools.ComponentsComposition;

namespace Core.Entities.Views
{
	public abstract class EntityView : ComponentOfRoot
	{
		public abstract void SetVisuals(VisualSettings settings);
	}
}