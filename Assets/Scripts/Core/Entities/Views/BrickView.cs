using Data.Levels.Visuals;
using UnityEngine;

namespace Core.Entities.Views
{
	public class BrickView : EntityView
	{
		private GameObject _currentVisuals;
		
		public override void SetVisuals(VisualSettings settings)
		{
			if (_currentVisuals != null) Destroy(_currentVisuals);
			
			GameObject[] views = settings.BrickViews;
			if (views.Length == 0) return;
			
			Brick brick = Root.Resolve<Brick>();
			GameObject viewPrefab = views[brick.Group % views.Length];
			_currentVisuals = Instantiate(viewPrefab, transform);

			EntityAnimator animator = Root.Resolve<EntityAnimator>();
			if (animator == null) return;
			animator.Play(EntityAnimations.Names.Idle, Random.value);
		}
	}
}