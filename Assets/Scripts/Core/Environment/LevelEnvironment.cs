using Core.Levels;
using UnityEngine;

namespace Core.Environment
{
	public class LevelEnvironment : MonoBehaviour
	{
		[SerializeField] private EnvironmentElement[] _elements;

		public void Init(Level level)
		{
			foreach (EnvironmentElement element in _elements) element.Init(level);
		}
	}
}