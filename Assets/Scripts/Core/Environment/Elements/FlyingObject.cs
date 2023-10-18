using Tools.Extensions;
using UnityEngine;

namespace Core.Environment.Elements
{
	public class FlyingObject : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;

		[Header("Movement")]
		[SerializeField] private Trajectory _trajectory;
		[SerializeField] private Vector2 _scaleRange = Vector2.one;
		[SerializeField] private Vector2 _amplitudeRange = new(0.5f, 1f);
		[SerializeField] private Vector2 _frequencyRange = new(0.25f, 1f);
		[SerializeField] private Vector2 _phaseRange = new(0, 1f);
		[SerializeField] private Vector2 _speedRange = new(2, 5);
		
		private Vector2 _direction;
		private float _amplitude;
		private float _frequency;
		private float _phase;
		private float _speed;
		private float _previousOffset;

		public void Init(Vector2 direction)
		{
			_direction = direction;
			transform.localScale = Vector3.one * _scaleRange.GetRandom();
			_amplitude = _amplitudeRange.GetRandom();
			_frequency = _frequencyRange.GetRandom();
			_phase = _phaseRange.GetRandom();
			_speed = _speedRange.GetRandom();
		}

		public Bounds GetBounds() => _renderer == null
			? new Bounds(transform.position, Vector3.zero)
			: _renderer.bounds;

		private void Update()
		{
			float offset = 0;
			if (_trajectory == Trajectory.Sinusoid)
			{
				float currentOffset = Mathf.Sin(_phase + _frequency * Time.time) * _amplitude;
				offset = currentOffset - _previousOffset;
				_previousOffset = currentOffset;
			}
			transform.position += _direction.XY0() * (_speed * Time.deltaTime) + Vector3.up * offset;
		}

		private enum Trajectory
		{
			Sinusoid,
			Straight
		}
	}
}