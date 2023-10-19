using Core.Entities;
using Core.Levels;
using Data.Entities;
using DG.Tweening;
using UnityEngine;

namespace Core.Services
{
	public class EntityMover
	{
		private readonly Level _level;
		private readonly EntitiesConfig _config;

		public EntityMover(Level level, EntitiesConfig config)
		{
			_level = level;
			_config = config;
		}

		public float Move(Move move)
		{
			Entity entity = move.Entity;
			Transform transform = entity.transform;
			Vector3 startPosition = transform.position;
			Vector3 endPosition = _level.Grid.ToWorld(move.EndPosition);
			float distance = (startPosition - endPosition).magnitude;
			float duration = distance / _config.Speed;
			
			entity.Position = move.EndPosition;
			DOTween.Kill(transform);
			transform.DOMove(endPosition, duration)
				.SetEase(Ease.Linear)
				.SetTarget(transform);
			
			return duration;
		}
	}
}