using UnityEngine;

namespace Core.Levels
{
	public class LevelCleaner
	{
		public void Clear(Level level)
		{
			if (level.Root != null) Object.Destroy(level.Root.gameObject);
		}
	}
}