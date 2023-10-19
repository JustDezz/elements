using Core.Levels;
using UnityEngine;

namespace Core.Environment
{
	public abstract class EnvironmentElement : MonoBehaviour
	{
		public abstract void Init(Level level);
	}
}