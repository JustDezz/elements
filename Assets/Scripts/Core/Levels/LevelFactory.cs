using Core.Entities;
using Data.Levels;
using UnityEngine;

namespace Core.Levels
{
	public class LevelFactory
	{
		private readonly EntitiesFactory _entitiesFactory;
		private readonly GridConfig _gridConfig;

		private Transform _levelRoot;

		public LevelFactory(EntitiesFactory entitiesFactory, GridConfig gridConfig)
		{
			_gridConfig = gridConfig;
			_entitiesFactory = entitiesFactory;
		}

		public Level Create(LevelDescription description)
		{
			_levelRoot = new GameObject(description.name).transform;
			
			CellGrid grid = new(_gridConfig.Origin, _gridConfig.CellSize, _gridConfig.CellGap);
			Entity[] entities = CreateEntities(grid, description.Entities);
			
			return new Level(description, entities, grid);
		}

		public void Clear(Level level)
		{
			if (_levelRoot != null) Object.Destroy(_levelRoot.gameObject);
		}

		private Entity[] CreateEntities(CellGrid grid, EntityDescription[] descriptions)
		{
			Entity[] entities = new Entity[descriptions.Length];
			for (int i = 0; i < descriptions.Length; i++)
			{
				EntityDescription description = descriptions[i];
				Entity entity = _entitiesFactory.Create(description.Data, _levelRoot);
				Vector2Int cellIndex = description.Position;
				entity.Position = cellIndex;
				entity.transform.position = grid.ToWorld(cellIndex, Vector2.zero);
				entities[i] = entity;
			}
			return entities;
		}
	}
}