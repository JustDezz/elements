using System;
using System.Collections.Generic;
using Core.Entities;
using Data.Entities;
using Tools.Extensions;
using UnityEngine;
using Zenject;

namespace Core.Levels
{
	public class EntitiesFactory
	{
		private readonly IInstantiator _instantiator;
		private readonly Dictionary<Type, Entity> _prefabs;

		public EntitiesFactory(EntitiesConfig config, IInstantiator instantiator)
		{
			_instantiator = instantiator;
			
			_prefabs = new Dictionary<Type, Entity>();
			foreach (Entity prefab in config.EntitiesPrefabs)
			{
				if (prefab == null) continue;
				
				Type dataType = GetDataType(prefab);
				if (dataType == null)
				{
					Debug.LogError($"Can't find required data type for {prefab.GetType()} {prefab.name}", prefab);
					continue;
				}
				
				_prefabs.Add(dataType, prefab);
			}
		}
		
		public Entity Create(EntityData entityData, Transform parent)
		{
			Entity prefab = _prefabs[entityData.GetType()];
			Entity entity = _instantiator.InstantiatePrefabForComponent<Entity>(prefab, parent);
			entity.SetGenericData(entityData);
			return entity;
		}

		private static Type GetDataType(Entity entity)
		{
			if (entity == null) return null;

			Type entityType = entity.GetType();
			Type baseType = entityType.BaseType;
			while (baseType != null)
			{
				foreach (Type argument in baseType.GenericTypeArguments)
					if (argument.Extends(typeof(EntityData)))
						return argument;

				baseType = baseType.BaseType;
			}

			return null;
		}
	}
}