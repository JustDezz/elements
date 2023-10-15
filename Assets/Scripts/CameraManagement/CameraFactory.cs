using UnityEngine;

namespace CameraManagement
{
	public class CameraFactory : ICameraFactory, ICameraService
	{
		private readonly CameraConfig _config;

		public CameraFactory(CameraConfig config) => _config = config;

		public GameCamera Camera { get; private set; }
		
		public GameCamera GetCamera()
		{
			if (Camera == null) Camera = Object.Instantiate(_config.CameraPrefab);
			return Camera;
		}
	}
}