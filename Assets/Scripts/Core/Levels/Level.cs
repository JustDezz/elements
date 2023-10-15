using Core.Entities;
using Data.Levels;
using UnityEngine;

namespace Core.Levels
{
	public class Level
	{
		public LevelDescription Description { get; }
		public Vector2Int Size => Description.Size;
		public Transform Root { get; }
		public Entity[] Entities { get; }
		public CellGrid Grid { get; }
		
		public Level(LevelDescription description, Transform root, Entity[] entities, CellGrid grid)
		{
			Root = root;
			Description = description;
			Entities = entities;
			Grid = grid;
		}
	}
}