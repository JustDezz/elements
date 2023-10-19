using Core.Entities;
using Data.Levels;
using UnityEngine;

namespace Core.Levels
{
	public class LevelFactory
	{
		private readonly EntitiesFactory _entitiesFactory;
		private readonly VisualsService _visualsService;
		private readonly GridConfig _gridConfig;

		public LevelFactory(EntitiesFactory entitiesFactory, VisualsService visualsService, GridConfig gridConfig)
		{
			_entitiesFactory = entitiesFactory;
			_visualsService = visualsService;
			_gridConfig = gridConfig;
		}

		public Level Create(LevelDescription description)
		{
			Level level = CreateLevel(description);
			CreateVisuals(description, level);
			return level;
		}

		private Level CreateLevel(LevelDescription description)
		{
			LevelCreationContext context = new()
			{
				Description = description,
				LevelRoot = new GameObject(description.name).transform,
				Grid = new CellGrid(_gridConfig.Origin, _gridConfig.CellSize, _gridConfig.CellGap)
			};

			context.Entities = CreateEntities(context);

			Level level = context.ToLevel();
			return level;
		}

		private void CreateVisuals(LevelDescription description, Level level) =>
			_visualsService.ApplyVisuals(description.Visuals, level);

		private Entity[] CreateEntities(LevelCreationContext context)
		{
			Transform entitiesRoot = new GameObject("Entities").transform;
			entitiesRoot.parent = context.LevelRoot;

			LevelDescription level = context.Description;
			EntityDescription[] descriptions = level.Entities;
			return _entitiesFactory.Create(descriptions, context.Grid, entitiesRoot, new Vector2Int(level.Padding, 0));
		}
	}
}