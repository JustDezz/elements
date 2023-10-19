using System;
using Core.Entities;

namespace Core.Services.NormalizationRules
{
	[Serializable]
	public class BrickPropagateRule : PropagateRule
	{
		protected override bool CanPropagate(Entity from, Entity to)
		{
			if (from is not Brick fromBrick || to is not Brick toBrick) return false;
			return fromBrick.Group == toBrick.Group;
		}
	}
}