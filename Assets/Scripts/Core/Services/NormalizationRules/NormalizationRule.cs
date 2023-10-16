using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Levels;
using UnityEngine;

namespace Core.Services.NormalizationRules
{
	[Serializable]
	public abstract class NormalizationRule
	{
		public abstract List<Entity> Normalize(Level level, Entity[,] entities, Vector2Int start);
	}
}