using UnityEngine;

namespace GameInput
{
	public struct Swipe
	{
		public Vector2 From { get; }
		public Vector2 To { get; }

		public Swipe(Vector2 from, Vector2 to)
		{
			From = from;
			To = to;
		}
	}
}