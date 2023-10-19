using Data.Levels.Visuals;
using UnityEngine;

namespace Core.Entities.Views
{
	public class StoneView : EntityView
	{
		[SerializeField] private SpriteRenderer _renderer;
		public override void SetVisuals(VisualSettings settings) => _renderer.color = settings.StoneColor;
	}
}