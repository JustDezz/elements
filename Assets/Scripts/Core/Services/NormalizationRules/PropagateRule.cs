using System.Collections.Generic;
using Core.Entities;
using Core.Levels;
using Tools.Extensions;
using UnityEngine;

namespace Core.Services.NormalizationRules
{
	public abstract class PropagateRule : NormalizationRule
	{
		protected abstract bool CanPropagate(Entity from, Entity to);
		
		public override List<Entity> Normalize(Level level, Entity[,] entities, Vector2Int start)
		{
			Entity startEntity = entities[start.x, start.y];
			return startEntity == null ? null : Propagate(entities, start);
		}

		private List<Entity> Propagate(Entity[,] entities, Vector2Int from)
		{
			HashSet<Vector2Int> visited = new();
			Dictionary<Vector2Int, Entity> result = new();
			PropagateRecursive(entities, from, visited, result);

			if (result.Count == 0) return null;
			List<Entity> propagated = new(result.Count);
			foreach ((Vector2Int _, Entity entity) in result) propagated.Add(entity);
			return propagated;
		}

		private void PropagateRecursive(
			Entity[,] entities, Vector2Int from, 
			HashSet<Vector2Int> visited, Dictionary<Vector2Int, Entity> result)
		{
			if (visited.Contains(from)) return;
			visited.Add(from);
			
			PropagateInDirection(entities, from, Vector2Int.up, visited, result);
			PropagateInDirection(entities, from, Vector2Int.right, visited, result);
		}

		private void PropagateInDirection(
			Entity[,] entities, Vector2Int from, Vector2Int direction,
			HashSet<Vector2Int> visited, Dictionary<Vector2Int, Entity> result)
		{
			int countForwards = TraceEntities(entities, from, direction);
			int countBackwards = TraceEntities(entities, from, -direction);
			int totalCount = countBackwards + 1 + countForwards;
			bool savePropagation = totalCount >= 3;

			for (int i = 0; i < totalCount; i++)
			{
				if (i == countBackwards) continue;
				Vector2Int position = from + direction * (i - countBackwards);
				Entity entity = entities[position.x, position.y];
				PropagateRecursive(entities, position, visited, result);
				if (!savePropagation) continue;
				if (result.ContainsKey(position)) continue;
				result.Add(position, entity);
			}
		}

		protected int TraceEntities(Entity[,] entities, Vector2Int from, Vector2Int direction)
		{
			if (direction == Vector2Int.zero) return 0;
			
			Entity previousEntity = entities[from.x, from.y];
			if (previousEntity == null) return 0;

			int count = 0;
			Vector2Int gridSize = entities.GetSize();
			Vector2Int position = from + direction;
			while (position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y)
			{
				Entity entity = entities[position.x, position.y];
				if (!CanPropagate(previousEntity, entity)) break;
				previousEntity = entity;
				position += direction;
				count++;
			}

			return count;
		}
	}
}