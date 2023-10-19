using UnityEngine;

namespace GameInput
{
	public class PCInput : InputBase
	{
		protected override bool HasInputInternal() => Input.GetMouseButton(0);
		public override Vector2 ScreenPosition() => Input.mousePosition;
	}
}