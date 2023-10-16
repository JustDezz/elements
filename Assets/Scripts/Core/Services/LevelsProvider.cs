using Data.Levels;

namespace Core.Services
{
	public class LevelsProvider
	{
		private readonly LevelDescription[] _levels;
		
		private int _currentIndex;

		public LevelsProvider(LevelsConfig levelsConfig) => _levels = levelsConfig.Levels;

		public LevelDescription Current => _levels[_currentIndex];

		public void MoveNext() => _currentIndex = WrapIndex(_currentIndex + 1);
		public void MoveBack() => _currentIndex = WrapIndex(_currentIndex - 1);
		public void MoveTo(int level) => _currentIndex = WrapIndex(level);

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