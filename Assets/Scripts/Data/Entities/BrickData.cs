using System;
using UnityEngine;

namespace Data.Entities
{
	[Serializable]
	public class BrickData : EntityData
	{
		[SerializeField] [Min(0)] private int _group;

		public int Group => _group;

		public BrickData() { }
		public BrickData(EntityData other) : base(other)
		{
			if (other is not BrickData brick) return;
			_group = brick._group;
		}

		public override EntityData Copy() => new BrickData(this);
	}
}