using UnityEngine;

namespace GameInput
{
	public interface IInput
	{
		public bool HasInput();
		public bool IsPointerOverUI();
		public Vector2 ScreenPosition();
	}
}