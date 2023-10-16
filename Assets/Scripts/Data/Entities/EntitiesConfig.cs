using UnityEngine;

namespace Data.Entities
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "Entities Config")]
	public class EntitiesConfig : ScriptableObject
	{
		[SerializeField] private float _speed;
		[SerializeField] private EntityConfig[] _entities;

		public float Speed => _speed;
		public EntityConfig[] Entities => _entities;
	}
}