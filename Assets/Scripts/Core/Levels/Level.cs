using Core.Entities;
using Data.Levels;

namespace Core.Levels
{
	public class Level
	{
		public LevelDescription Description { get; }
		public Entity[] Entities { get; }
		public CellGrid Grid { get; }
		
		public Level(LevelDescription description, Entity[] entities, CellGrid grid)
		{
			Description = description;
			Entities = entities;
			Grid = grid;
		}
	}
}