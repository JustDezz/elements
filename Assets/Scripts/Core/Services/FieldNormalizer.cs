using System;
using System.Threading;
using Core.Entities;
using Core.Levels;
using Cysharp.Threading.Tasks;
using Tools.Extensions;
using UnityEngine;

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
			
			_entities = new Entity[_level.Size.x + 2, _level.Size.y];
			SyncGrid();
		}

		public async UniTask Normalize(CancellationToken ct)
		{
			SyncGrid();
			while (!ct.IsCancellationRequested)
			{
				await DropEntities(ct);
				break;
			}
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
				Vector2Int endPosition = IndexToPosition(endIndex);
				Move move = new(entity, endPosition);

				entity.Position = IndexToPosition(endPosition);
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
				_entities[x, y] = null;

			foreach (Entity entity in _level.Entities)
			{
				Vector2Int position = entity.Position;
				Vector2Int index = PositionToIndex(position);
				_entities[index.x, index.y] = entity;
			}
		}

		private static Vector2Int PositionToIndex(Vector2Int position) => position + Vector2Int.right;
		private static Vector2Int IndexToPosition(Vector2Int position) => position - Vector2Int.right;
	}
}