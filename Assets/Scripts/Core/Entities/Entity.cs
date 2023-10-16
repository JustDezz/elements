using System;
using Data.Entities;
using Tools.ComponentsComposition;
using UnityEngine;

namespace Core.Entities
{
	public abstract class Entity : ComponentsRoot
	{
		public Vector2Int Position { get; set; }
		
		public abstract void SetGenericData(EntityData data);
		public abstract Type GetDataType();
	}
}