using Core.Entities;
using Data.Levels;
using UnityEngine;

namespace Core.Levels
{
	public class LevelCreationContext
	{
		public LevelDescription Description { get; set; }
		public Transform LevelRoot { get; set; }
		public Transform EntitiesRoot { get; set; }
		public Transform EnvironmentRoot { get; set; }
		
		public CellGrid Grid { get; set; }
		public Entity[] Entities { get; set; }

		public Level ToLevel() => new(Description, LevelRoot, Entities, Grid);
	}
}