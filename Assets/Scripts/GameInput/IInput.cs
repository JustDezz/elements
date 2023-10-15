using System;
using UnityEngine;

namespace GameInput
{
	public interface IInput
	{
		public event Action<Swipe> Swiped;

		public bool HasInput();
		public bool IsPointerOverUI();
		public Vector2 ScreenPosition();
	}
}