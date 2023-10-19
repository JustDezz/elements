using UnityEngine;
using UnityEngine.EventSystems;

namespace GameInput
{
	public abstract class InputBase : IInput
	{
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
		
		public abstract Vector2 ScreenPosition();
		protected abstract bool HasInputInternal();
	}
}