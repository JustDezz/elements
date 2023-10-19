using Data.Levels;
using Tools.PlayerPrefsProperties;

namespace Core.Services
{
	public class LevelsProvider
	{
		private readonly LevelDescription[] _levels;

		public PlayerPrefsIntProperty CurrentIndex { get; }

		public LevelsProvider(LevelsConfig levelsConfig)
		{
			_levels = levelsConfig.Levels;
			
			// I have not enough time to create decent save system, so I will use PlayerPrefs
			CurrentIndex = new PlayerPrefsIntProperty("LevelsProvider_CurrentIndex", 0);
		}

		public LevelDescription Current => _levels[CurrentIndex];

		public void MoveNext() => CurrentIndex.Value = WrapIndex(CurrentIndex + 1);
		public void MoveBack() => CurrentIndex.Value = WrapIndex(CurrentIndex - 1);
		public void MoveTo(int level) => CurrentIndex.Value = WrapIndex(level);

		private int WrapIndex(int index)
		{
			int levelsCount = _levels.Length;
			if (levelsCount == 0) return 0;
			index %= levelsCount;
			if (index < 0) index = levelsCount + index;
			return index;
		}
	}
}