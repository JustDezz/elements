using System;
using Data.Entities;

namespace Core.Entities
{
	public class Stone : GenericEntity<Stone.StoneData>
	{
		protected override void SetData(StoneData entityData)
		{
		}
		
		[Serializable]
		public class StoneData : EntityData
		{
			public StoneData() { }
			private StoneData(EntityData other) : base(other) { }
			public override EntityData Copy() => new StoneData(this);
		}
	}
}