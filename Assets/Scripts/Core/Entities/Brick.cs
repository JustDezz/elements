using Data.Entities;

namespace Core.Entities
{
	public class Brick : GenericEntity<BrickData>
	{
		public int Group { get; private set; }

		protected override void SetData(BrickData entityData) => Group = entityData.Group;
	}
}