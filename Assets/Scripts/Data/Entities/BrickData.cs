using System;
using UnityEngine;

namespace Data.Entities
{
	[Serializable]
	public class BrickData : EntityData
	{
		[SerializeField] [Min(0)] private int _group;

		public int Group => _group;
	}
}