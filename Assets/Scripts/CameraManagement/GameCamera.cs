using Tools.Extensions;
using UnityEngine;

namespace CameraManagement
{
	public class GameCamera : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		
		private Bounds _bounds;

		public Camera Camera => _camera;
		public CameraConfig Config { get; set; }

		public void Frame(Bounds bounds)
		{
			_bounds = bounds;
			if (Config == null) return;
			Quaternion rotation = Quaternion.Euler(Config.Rotation);
			Vector3 forward = rotation * Vector3.forward;
			Vector3 up = rotation * Vector3.up;
			Vector3 right = rotation * Vector3.right;

			Bounds projectedBounds = bounds.ProjectOnPlane(bounds.center, -forward, up, right, false);
			Vector2 frameSize = projectedBounds.size;

			Vector2 horizontalViewport = Config.HorizontalViewport;
			Vector2 verticalViewport = Config.VerticalViewport;
			float cameraSize = GetCameraSize(frameSize, horizontalViewport, verticalViewport);

			float horizontalViewportOffset = GetViewportOffset(horizontalViewport);
			float verticalViewportOffset = GetViewportOffset(verticalViewport);
			float viewportHeight = verticalViewport.y - verticalViewport.x;
			float occupiedVerticalViewport = frameSize.y / (cameraSize * 2);
			float verticalOffset = viewportHeight - occupiedVerticalViewport;

			Vector2 viewportOffset = new(horizontalViewportOffset / 2, verticalViewportOffset + verticalOffset);
			Vector3 offset = (viewportOffset.x * right + viewportOffset.y * up) * cameraSize;
			Vector3 position = bounds.center - forward * Config.Distance + offset;

			_camera.orthographicSize = cameraSize;
			transform.SetPositionAndRotation(position, rotation);
		}

		private void Update() => Frame(_bounds);
		
		private float GetCameraSize(Vector2 frameSize, Vector2 horizontalViewport, Vector2 verticalViewport)
		{
			float viewportWidth = horizontalViewport.y - horizontalViewport.x;
			float viewportHeight = verticalViewport.y - verticalViewport.x;

			float desiredWidth = frameSize.x / Mathf.Max(CameraConfig.MinViewportSize, viewportWidth);
			float desiredHeight = frameSize.y / Mathf.Max(CameraConfig.MinViewportSize, viewportHeight);

			return Mathf.Max(desiredHeight, desiredWidth / _camera.aspect) / 2;
		}
		private static float GetViewportOffset(Vector2 viewport) => 1 - viewport.y - viewport.x;
	}
}