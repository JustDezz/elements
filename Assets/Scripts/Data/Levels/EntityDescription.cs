using System;
using Data.Entities;
using UnityEngine;

namespace Data.Levels
{
	[Serializable]
	public struct EntityDescription
	{
		[SerializeField] [Min(0)] private Vector2Int _position;
		[SerializeReference] private EntityData _data;

		public Vector2Int Position => _position;
		public EntityData Data
		{
			get => _data;
			set => _data = value;
		}
	}
}