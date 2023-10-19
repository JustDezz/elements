using Data.Levels.Visuals;
using DG.Tweening;
using Tools.ComponentsComposition;
using UnityEngine;

namespace Core.Entities.Views
{
	public abstract class EntityView : ComponentOfRoot
	{
		public abstract void SetVisuals(VisualSettings settings);

		public virtual void SetPressed(bool isPressed)
		{
			DOTween.Kill(this);
			Vector3 targetScale = Vector3.one;
			if (isPressed) targetScale *= 1.1f;
			transform.DOScale(targetScale, 0.2f)
				.SetEase(Ease.OutBack)
				.SetTarget(this);
		}
	}
}