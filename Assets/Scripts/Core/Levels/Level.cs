using System.Linq;
using Core.Entities;
using Data.Levels;
using UnityEngine;

namespace Core.Levels
{
	public class Level
	{
		public LevelDescription Description { get; }
		public Vector2Int Size { get; }
		public Bounds Bounds { get; }
		public Transform Root { get; }
		public Entity[] Entities { get; }
		public CellGrid Grid { get; }
		
		public Level(LevelDescription description, Transform root, Entity[] entities, CellGrid grid)
		{
			Description = description;
			Root = root;
			Entities = entities;
			Grid = grid;
			Size = Description.Size + new Vector2Int(Description.Padding * 2, 0);
			Bounds = new Bounds
			{
				min = Grid.ToWorld(Vector2Int.zero, Vector2.zero),
				max = Grid.ToWorld(Size, Vector2.zero)
			};
		}

		public Entity GetEntityAtPosition(Vector2Int position) => Entities.FirstOrDefault(e => e != null && e.Position == position);
	}
}