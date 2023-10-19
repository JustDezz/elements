using System;

namespace Data.Entities
{
	[Serializable]
	public abstract class EntityData
	{
		public EntityData() {}
		public EntityData(EntityData other) {}

		public abstract EntityData Copy();
	}
}