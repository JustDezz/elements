using Data.Entities;
using UnityEngine;

namespace Core.Entities
{
	public abstract class Entity : MonoBehaviour
	{
		public Vector2Int Position { get; set; }
		
		public abstract void SetGenericData(EntityData data);
	}
}