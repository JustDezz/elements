using UnityEngine;

namespace GameInput
{
	public class MobileInput : InputBase
	{
		protected override bool HasInputInternal() => Input.touchCount > 0;
		public override Vector2 ScreenPosition() => !HasInputInternal() ? default : Input.touches[0].position;
	}
}