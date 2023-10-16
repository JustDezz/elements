using System.Linq;
using System.Threading;
using CameraManagement;
using Core.Entities;
using Core.Entities.Views;
using Core.Levels;
using Cysharp.Threading.Tasks;
using GameInput;
using UnityEngine;

namespace Core.Services
{
	public class MoveMaker
	{
		private readonly Level _level;
		private readonly IInput _input;
		private readonly GameCamera _camera;

		public MoveMaker(Level level, IInput input, ICameraService camera)
		{
			_camera = camera.Camera;
			_level = level;
			_input = input;
		}

		public async UniTask<Move> MakeMove(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				await UniTask.Yield(ct);
				await UniTask.WaitUntil(_input.HasInput, cancellationToken: ct);
				
				Vector2 startPosition = _input.ScreenPosition();
				Entity entity = GetEntityAtPosition(startPosition);
				if (entity == null) continue; 
				if (entity.TryResolve(out EntityView view)) view.SetPressed(true);

				Vector2 endPosition = startPosition;
				while (!ct.IsCancellationRequested && _input.HasInput())
				{
					endPosition = _input.ScreenPosition();
					await UniTask.Yield(ct);
				}

				if (view != null) view.SetPressed(false);
				if (GetEntityAtPosition(endPosition) == entity) continue;

				Vector2 swipe = endPosition - startPosition;
				Vector2Int moveDirection = GetMoveDirection(swipe);
				if (ValidateMove(entity, moveDirection)) return new Move(entity, entity.Position + moveDirection);
			}

			return default;
		}

		private static Vector2Int GetMoveDirection(Vector2 swipe) =>
			Mathf.Abs(swipe.x) >= Mathf.Abs(swipe.y)
				? new Vector2Int((int) Mathf.Sign(swipe.x), 0)
				: new Vector2Int(0, (int) Mathf.Sign(swipe.y));

		private bool ValidateMove(Entity entity, Vector2Int direction)
		{
			if (direction == Vector2Int.zero) return false;
			
			Vector2Int startPosition = entity.Position;
			Vector2Int endPosition = startPosition + direction;
			if (direction.y == 1)
			{
				Entity above = _level.Entities.FirstOrDefault(e => e.Position == endPosition);
				if (above == null) return false;
			}

			if (direction.y == -1) return endPosition.y >= 0;

			return endPosition.x >= 0 && endPosition.x < _level.Size.x;
		}

		private Entity GetEntityAtPosition(Vector2 screenPosition)
		{
			Ray ray = _camera.Camera.ScreenPointToRay(screenPosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
			return hit.collider == null ? null : hit.collider.GetComponent<Entity>();
		}
	}
}