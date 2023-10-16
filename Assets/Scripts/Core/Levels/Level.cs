using Core.Entities;
using Data.Levels;
using UnityEngine;

namespace Core.Levels
{
	public class Level
	{
		public LevelDescription Description { get; }
		public Vector2Int Size { get; }
		public Transform Root { get; }
		public Entity[] Entities { get; }
		public CellGrid Grid { get; }
		
		public Level(LevelDescription description, Transform root, Entity[] entities, CellGrid grid)
		{
			Root = root;
			Description = description;
			Entities = entities;
			Size = Description.Size + new Vector2Int(Description.Padding * 2, 0);
			Grid = grid;
		}
	}
}