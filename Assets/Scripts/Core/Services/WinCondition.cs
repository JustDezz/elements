using Core.Entities;
using Core.Levels;
using Data.Entities;

namespace Core.Services
{
	public class WinCondition
	{
		private readonly Level _level;
		private readonly EntityConfigsProvider _configsProvider;

		public WinCondition(Level level, EntityConfigsProvider configsProvider)
		{
			_level = level;
			_configsProvider = configsProvider;
		}

		public bool IsMet()
		{
			foreach (Entity entity in _level.Entities)
			{
				if (entity == null) continue;
				EntityConfig config = _configsProvider.GetConfig(entity);
				if (config == null) continue;
				if (config.IsRequiredForLevelCompletion) return false;
			}

			return true;
		}
	}
}