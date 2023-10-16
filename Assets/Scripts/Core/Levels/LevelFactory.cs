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
			LevelCreationContext context = new()
			{
				Description = description,
				LevelRoot = new GameObject(description.name).transform,
				Grid = new CellGrid(_gridConfig.Origin, _gridConfig.CellSize, _gridConfig.CellGap)
			};

			context.Entities = CreateEntities(context);
			_visualsService.ApplyVisuals(description.Visuals, context.Entities);
			
			return context.ToLevel();
		}

		private Entity[] CreateEntities(LevelCreationContext context)
		{
			Transform entitiesRoot = new GameObject("Entities").transform;
			entitiesRoot.parent = context.LevelRoot;
			context.EntitiesRoot = entitiesRoot;

			LevelDescription level = context.Description;
			EntityDescription[] descriptions = level.Entities;
			return _entitiesFactory.Create(descriptions, context.Grid, entitiesRoot, new Vector2Int(level.Padding, 0));
		}
	}
}