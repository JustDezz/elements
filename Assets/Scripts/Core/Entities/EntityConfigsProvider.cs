using System;
using System.Collections.Generic;
using Data.Entities;

namespace Core.Entities
{
	public class EntityConfigsProvider
	{
		private readonly Dictionary<Type, EntityConfig> _configsByData;
		private readonly Dictionary<Type, EntityConfig> _configsByEntities;

		public EntityConfigsProvider(EntitiesConfig config)
		{
			_configsByData = new Dictionary<Type, EntityConfig>();
			_configsByEntities = new Dictionary<Type, EntityConfig>();
			foreach (EntityConfig entity in config.Entities)
			{
				if (entity == null) continue;
				Entity prefab = entity.Prefab;
				_configsByData.Add(prefab.GetDataType(), entity);
				_configsByEntities.Add(prefab.GetType(), entity);
			}
		}

		public EntityConfig GetConfigByData<TData>() where TData : EntityData => GetConfigByData(typeof(TData));
		public EntityConfig GetConfigByData(Type data) => _configsByData.TryGetValue(data, out EntityConfig config) ? config : null;

		public EntityConfig GetConfigByEntity<TEntity>() where TEntity : Entity => GetConfigByEntity(typeof(TEntity));
		public EntityConfig GetConfig(Entity entity) => entity != null ? GetConfigByEntity(entity.GetType()) : null;
		public EntityConfig GetConfigByEntity(Type entity) => _configsByEntities.TryGetValue(entity, out EntityConfig config) ? config : null;
	}
}