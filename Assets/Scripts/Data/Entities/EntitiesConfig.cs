using Core.Entities;
using UnityEngine;

namespace Data.Entities
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "Entities Config")]
	public class EntitiesConfig : ScriptableObject
	{
		[SerializeField] private float _speed;
		[SerializeField] private Entity[] _entitiesPrefabs;

		public float Speed => _speed;
		public Entity[] EntitiesPrefabs => _entitiesPrefabs;
	}
}