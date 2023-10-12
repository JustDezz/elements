using Core.Entities;
using UnityEngine;

namespace Data.Entities
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "Entities Config")]
	public class EntitiesConfig : ScriptableObject
	{
		[SerializeField] private Entity[] _entitiesPrefabs;

		public Entity[] EntitiesPrefabs => _entitiesPrefabs;
	}
}