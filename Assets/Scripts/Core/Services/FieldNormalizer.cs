using System;
using System.Collections.Generic;
using System.Threading;
using Core.Entities;
using Core.Levels;
using Core.Services.NormalizationRules;
using Cysharp.Threading.Tasks;
using Tools.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Services
{
	public class FieldNormalizer
	{
		private readonly Level _level;
		private readonly EntityMover _mover;
		private readonly Entity[,] _entities;

		public FieldNormalizer(Level level, EntityMover mover)
		{
			_level = level;
			_mover = mover;

			_entities = new Entity[_level.Size.x, _level.Size.y];
			SyncGrid();
		}

		public async UniTask Normalize(CancellationToken ct)
		{
			SyncGrid();
			while (!ct.IsCancellationRequested)
			{
				await DropEntities(ct);
				if (await Destroy(ct)) continue;
				break;
			}
		}

		private async UniTask<bool> Destroy(CancellationToken ct)
		{
			Vector2Int gridSize = _entities.GetSize();
			NormalizationRule rule = new BrickPropagateRule();
			HashSet<Vector2Int> toDestroy = new();
			for (int x = 0; x < gridSize.x; x++)
			for (int y = 0; y < gridSize.y; y++)
			{
				Vector2Int start = new(x, y);
				if (toDestroy.Contains(start)) continue;

				List<Entity> normalized = rule.Normalize(_level, _entities, start);
				if (normalized == null || normalized.Count == 0) continue;

				foreach (Entity entity in normalized) toDestroy.Add(entity.Position);
			}

			if (toDestroy.Count <= 0) return false;

			float delay = 0;
			foreach (Vector2Int position in toDestroy)
			{
				Entity entity = _entities[position.x, position.y];
				if (!entity.TryResolve(out EntityAnimator animator)) continue;
				animator.Play(EntityAnimations.Names.Destroy);
				delay = Mathf.Max(delay, animator.GetCurrentAnimationDuration());
			}

			await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: ct);
			foreach (Vector2Int position in toDestroy)
			{
				Entity entity = _entities[position.x, position.y];
				_entities[position.x, position.y] = default;
				Object.Destroy(entity.gameObject);
			}

			return true;

		}

		private async UniTask DropEntities(CancellationToken ct)
		{
			float duration = 0;
			Vector2Int gridSize = _entities.GetSize();
			for (int x = 0; x < gridSize.x; x++)
			for (int y = 0; y < gridSize.y; y++)
			{
				Entity entity = _entities[x, y];
				if (entity != null) continue;
				float columnDropDuration = DropColumn(x, y);
				duration = Mathf.Max(duration, columnDropDuration);
				break;
			}

			await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: ct);
		}

		private float DropColumn(int column, int dropTo)
		{
			float duration = 0;
			int droppedEntities = 0;
			for (int y = dropTo + 1; y < _entities.GetLength(1); y++)
			{
				Entity entity = _entities[column, y];
				if (entity == null) continue;
				Vector2Int endIndex = new(column, dropTo + droppedEntities);
				_entities[endIndex.x, endIndex.y] = entity;
				_entities[column, y] = default;

				Move move = new(entity, endIndex);
				duration = _mover.Move(move);
				droppedEntities++;
			}

			return duration;
		}

		private void SyncGrid()
		{
			Vector2Int gridSize = _entities.GetSize();
			for (int x = 0; x < gridSize.x; x++)
			for (int y = 0; y < gridSize.y; y++)
				_entities[x, y] = default;

			foreach (Entity entity in _level.Entities)
			{
				Vector2Int position = entity.Position;
				_entities[position.x, position.y] = entity;
			}
		}
	}
}