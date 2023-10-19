using System;
using Data.Entities;
using UnityEngine;

namespace Data.Levels
{
	[Serializable]
	public class EntityDescription
	{
		[SerializeField] private Vector2Int _position;
		[SerializeReference] [SubclassSelector] private EntityData _data;

		public Vector2Int Position => _position;
		public EntityData Data
		{
			get => _data;
			set => _data = value;
		}
		
		public EntityDescription() { }
		public EntityDescription(Vector2Int position) => _position = position;
		public EntityDescription(EntityDescription other)
		{
			if (other == null) _position = Vector2Int.zero;
			else
			{
				_position = other._position;
				_data = other._data?.Copy();
			}
		}
		public EntityDescription(EntityDescription other, Vector2Int position)
		{
			_position = position;
			_data = other._data?.Copy();
		}
	}
}