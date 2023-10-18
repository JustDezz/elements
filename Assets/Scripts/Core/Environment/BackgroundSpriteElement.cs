using CameraManagement;
using Core.Levels;
using Tools.Extensions;
using UnityEngine;
using Zenject;

namespace Core.Environment
{
	public class BackgroundSpriteElement : EnvironmentElement
	{
		[SerializeField] private SpriteRenderer _renderer;
		[SerializeField] [Range(0, 0.9f)] private float _groundHeight;
		[SerializeField] [Min(0)] private float _cameraDistance = 10;
		
		private ICameraService _cameraService;

		[Inject]
		public void Construct(ICameraService cameraService) => _cameraService = cameraService;
		public override void Init(Level level) { }

		private void Update()
		{
			GameCamera gameCamera = _cameraService?.Camera;
			if (gameCamera == null) return;
			Camera cam = gameCamera.Camera;
			Transform camTransform = cam.transform;
			Vector3 forward = camTransform.forward;
			Vector3 planeOrigin = camTransform.position + forward * _cameraDistance;

			Bounds bounds = GetPlaneSpaceBounds(-forward, planeOrigin, cam);
			Bounds spriteBounds = _renderer.sprite.bounds;
			Vector3 boundsSize = bounds.size;
			Vector3 spriteSize = spriteBounds.size;
			float spriteAspect = spriteSize.x / spriteSize.y;
			float targetAspect = boundsSize.x / boundsSize.y;

			Vector3 size = spriteAspect < targetAspect
				? new Vector3(boundsSize.x, boundsSize.x / spriteAspect, 1)
				: new Vector3(boundsSize.y * spriteAspect, boundsSize.y, 1);
			
			Vector3 scale = size.Divide(new Vector3(spriteSize.x, spriteSize.y, 1));
			(Vector3 position, Vector3 scale) offset = GetGroundOffset(camTransform.up, boundsSize, size, gameCamera.Config.VerticalViewport);

			Transform t = transform;
			t.SetPositionAndRotation(planeOrigin + offset.position, camTransform.rotation);
			t.localScale = scale + offset.scale;
		}

		private (Vector3 position, Vector3 scale) GetGroundOffset(Vector3 up, Vector3 cameraSize, Vector3 spriteSize, Vector2 viewport)
		{
			float groundViewport = viewport.x;
			if (_groundHeight < groundViewport) return default;
			float spriteViewportSize = spriteSize.y / cameraSize.y;
			float toZeroViewport = spriteViewportSize / 2 - 0.5f;
			float toGroundViewport = groundViewport - _groundHeight * spriteViewportSize;
			float deltaViewport = toZeroViewport + toGroundViewport;
			float spriteOversize = spriteViewportSize - 1;

			float viewportAbs = Mathf.Abs(deltaViewport);
			float deltaScale = viewportAbs > spriteOversize ? viewportAbs - spriteOversize : 0;
			float deltaPosition = deltaViewport * cameraSize.y + spriteSize.y * deltaScale / 3;
			
			Vector3 position = up * deltaPosition;
			Vector3 scale = new(deltaScale, deltaScale, 0);

			return (position, scale);
		}

		private Bounds GetPlaneSpaceBounds(Vector3 normal, Vector3 planeOrigin, Camera cam)
		{
			Plane plane = new(normal, planeOrigin);
			Vector3 worldLowerLeft = GetPointOnPlane(plane, cam, new Vector3(0, 0));
			Vector3 worldLowerRight = GetPointOnPlane(plane, cam, new Vector3(1, 0));
			Vector3 worldTopLeft = GetPointOnPlane(plane, cam, new Vector3(0, 1));
			Vector3 worldTopRight = GetPointOnPlane(plane, cam, new Vector3(1, 1));

			Transform camTransform = cam.transform;
			Vector3 up = camTransform.up;
			Vector3 right = camTransform.right;

			Vector2 planeLowerLeft = worldLowerLeft.Rebase(planeOrigin, up, right);
			Vector2 planeLowerRight = worldLowerRight.Rebase(planeOrigin, up, right);
			Vector2 planeTopLeft = worldTopLeft.Rebase(planeOrigin, up, right);
			Vector2 planeTopRight = worldTopRight.Rebase(planeOrigin, up, right);

			Vector2 min = planeLowerLeft;
			min = Vector2.Min(min, planeLowerRight);
			min = Vector2.Min(min, planeTopLeft);
			min = Vector2.Min(min, planeTopRight);

			Vector2 max = planeLowerLeft;
			max = Vector2.Max(max, planeLowerRight);
			max = Vector2.Max(max, planeTopLeft);
			max = Vector2.Max(max, planeTopRight);

			Bounds bounds = new();
			bounds.SetMinMax(min, max);
			return bounds;
		}

		private Vector3 GetPointOnPlane(Plane plane, Camera cam, Vector3 viewport)
		{
			Ray ray = cam.ViewportPointToRay(viewport);
			plane.Raycast(ray, out float enter);
			return ray.GetPoint(enter);
		}
	}
}