using Core.Levels;
using Data.Entities;
using Data.Levels;
using Tools.Extensions;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
	public class EntitiesFactory
	{
		private readonly IInstantiator _instantiator;
		private readonly EntityConfigsProvider _configsProvider;

		public EntitiesFactory(IInstantiator instantiator, EntityConfigsProvider configsProvider)
		{
			_configsProvider = configsProvider;
			_instantiator = instantiator;
		}

		public Entity[] Create(EntityDescription[] descriptions, CellGrid grid, Transform parent, Vector2Int padding)
		{
			Entity[] entities = new Entity[descriptions.Length];

			for (int i = 0; i < descriptions.Length; i++)
			{
				EntityDescription description = descriptions[i];
				Vector2Int cellIndex = description.Position + padding;
				Entity entity = Create(description.Data, parent);
				
				Transform entityTransform = entity.transform;
				entity.Position = cellIndex;
				entityTransform.position = grid.ToWorld(cellIndex);
				entityTransform.localScale = grid.CellSize.XY1();
				entities[i] = entity;
			}
			
			return entities;
		}
		
		public Entity Create(EntityData entityData, Transform parent)
		{
			EntityConfig config = _configsProvider.GetConfigByData(entityData.GetType());
			Entity prefab = config.Prefab;
			Entity entity = _instantiator.InstantiatePrefabForComponent<Entity>(prefab, parent);
			entity.SetGenericData(entityData);
			return entity;
		}
	}
}