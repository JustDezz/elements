using System;
using System.Collections.Generic;
using CameraManagement;
using Core.Levels;
using Tools.Extensions;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Environment.Elements
{
	public class FlyingObjectsBackground : EnvironmentElement
	{
		[SerializeField] private FlyingObject[] _prefabs;
		[SerializeField] [Min(0)] private int _maxElements;
		[SerializeField] [Min(0)] private Vector2Int _spawnAtStart;
		[SerializeField] private float _spawnDelay;

		[Space]
		[SerializeField] private SpawnSides _spawnSides;
		[SerializeField] private Vector2 _viewportRange;
		[SerializeField] private float _cameraDistance = 10;

		private ICameraService _cameraService;
		private List<FlyingObject> _elements;
		private float _lastSpawnTime;
		
		private static readonly Plane[] Planes = new Plane[6];

		[Inject]
		public void Construct(ICameraService cameraService)
		{
			_cameraService = cameraService;
			_elements = new List<FlyingObject>(_maxElements);
		}

		private void Start()
		{
			int toSpawn = _spawnAtStart.GetRandom();
			if (toSpawn == 0) return;
			_lastSpawnTime = Time.time;
			for (int i = 0; i < toSpawn; i++) _elements.Add(SpawnElement(_cameraService.Camera));
		}

		public override void Init(Level level) { }

		private void Update()
		{
			GameCamera gameCamera = _cameraService?.Camera;
			if (gameCamera == null) return;
			if (_elements == null) return;
			
			DeleteInvisibleElements(gameCamera);
			if (_elements.Count >= _maxElements) return;
			if (Time.time - _lastSpawnTime < _spawnDelay) return;
			
			FlyingObject element = SpawnElement(gameCamera);
			_elements.Add(element);
			_lastSpawnTime = Time.time;
		}

		private FlyingObject SpawnElement(GameCamera gameCamera)
		{
			float verticalViewport = _viewportRange.GetRandom();
			float horizontalViewport = _spawnSides switch
			{
				SpawnSides.Left => 0,
				SpawnSides.Right => 1,
				SpawnSides.Random => Random.value < 0.5f ? 0 : 1,
				_ => throw new ArgumentOutOfRangeException()
			};

			Camera cam = gameCamera.Camera;
			Transform camTransform = cam.transform;
			Vector3 forward = camTransform.forward;

			Plane plane = new(-forward, camTransform.position + forward * _cameraDistance);
			Ray ray = cam.ViewportPointToRay(new Vector3(horizontalViewport, verticalViewport));
			plane.Raycast(ray, out float enter);
			Vector3 spawnPoint = ray.GetPoint(enter);

			FlyingObject prefab = _prefabs.GetRandom();
			FlyingObject element = Instantiate(prefab, spawnPoint, Quaternion.identity, transform);
			Vector2 movementDirection = new(Mathf.Sign(0.5f - horizontalViewport), 0);
			element.Init(movementDirection);
			Bounds bounds = element.GetBounds();
			element.transform.position += new Vector3(-movementDirection.x * bounds.extents.x * 1.99f, 0); 
			return element;
		}

		private void DeleteInvisibleElements(GameCamera gameCamera)
		{
			GeometryUtility.CalculateFrustumPlanes(gameCamera.Camera, Planes);
			for (int i = 0; i < _elements.Count; i++)
			{
				FlyingObject element = _elements[i];
				Bounds bounds = element.GetBounds();
				bounds.Expand(bounds.extents.x * 2);
				if (GeometryUtility.TestPlanesAABB(Planes, bounds)) continue;
				Destroy(element.gameObject);
				_elements.RemoveAt(i);
			}
		}

		private enum SpawnSides
		{
			Left,
			Right,
			Random
		}
	}
}