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
		
		// Each entity can define its own normalization rules or reuse already existing ones
		// It allows for great a flexibility. For example, you can add dynamite or classic boosters from match-3 games
	}
}