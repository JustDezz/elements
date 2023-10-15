using Data;
using UnityEngine;

namespace CameraManagement
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "Camera Config")]
	public class CameraConfig : ScriptableObject
	{
		[SerializeField] private GameCamera _cameraPrefab;

		[Space]
		[SerializeField] private float _distance;
		[SerializeField] private Vector2 _horizontalViewport;
		[SerializeField] private Vector2 _verticalViewport;
		[SerializeField] private Vector3 _rotation;
		
		public const float MinViewportSize = 0.01f;

		public GameCamera CameraPrefab => _cameraPrefab;

		public float Distance => _distance;
		public Vector2 HorizontalViewport => _horizontalViewport;
		public Vector2 VerticalViewport => _verticalViewport;
		public Vector3 Rotation => _rotation;

		private void OnValidate()
		{
			_horizontalViewport = ClampViewport(_horizontalViewport);
			_verticalViewport = ClampViewport(_verticalViewport);
		}

		private static Vector2 ClampViewport(Vector2 viewport)
		{
			viewport.x = Mathf.Clamp(viewport.x, 0, Mathf.Max(viewport.y - MinViewportSize, MinViewportSize));
			viewport.y = Mathf.Clamp(viewport.y, Mathf.Min(viewport.x + MinViewportSize, 1 - MinViewportSize), 1);
			return viewport;
		}
	}
}