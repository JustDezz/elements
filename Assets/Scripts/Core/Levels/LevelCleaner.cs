using UnityEngine;

namespace Core.Levels
{
	public class LevelCleaner
	{
		public void Clear(Level level)
		{
			if (level == null) return;
			if (level.Root != null) Object.Destroy(level.Root.gameObject);
		}
	}
}