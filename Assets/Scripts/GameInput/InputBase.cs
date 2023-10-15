using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameInput
{
	public abstract class InputBase : IInput
	{
		public event Action<Swipe> Swiped;
		
		public bool HasInput()
		{
			if (!HasInputInternal()) return false;
			return !IsPointerOverUI();
		}

		public bool IsPointerOverUI()
		{
			EventSystem eventSystem = EventSystem.current;
			return eventSystem != null && eventSystem.IsPointerOverGameObject();
		}

		public async UniTaskVoid InputLoop(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				await UniTask.WaitUntil(HasInput, cancellationToken: ct);
				Vector2 startPoint = ScreenPosition();
				Vector2 endPoint = startPoint;
				while (!ct.IsCancellationRequested && HasInput())
				{
					endPoint = ScreenPosition();
					await UniTask.Yield(ct);
				}

				Swiped?.Invoke(new Swipe(startPoint, endPoint));
			}
		}
		
		public abstract Vector2 ScreenPosition();
		protected abstract bool HasInputInternal();
	}
}