using Core.Entities;
using UnityEngine;

namespace Core.Services
{
	public struct Move
	{
		public Entity Entity { get; }
		public Vector2Int StartPosition { get; }
		public Vector2Int EndPosition { get; }

		public Move(Entity entity, Vector2Int endPosition)
		{
			Entity = entity;
			StartPosition = Entity.Position;
			EndPosition = endPosition;
		}
	}
}