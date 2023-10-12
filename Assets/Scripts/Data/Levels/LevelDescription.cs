using Data.Entities;
using UnityEngine;

namespace Data.Levels
{
	[CreateAssetMenu(menuName = SOConstants.Menu + "New Level")]
	public class LevelDescription : ScriptableObject
	{
		[SerializeField] private EntityDescription[] _entities;

		public EntityDescription[] Entities => _entities;

		private void OnValidate()
		{
			for (var i = 0; i < _entities.Length; i++)
			{
				EntityDescription entity = _entities[i];
				entity.Data ??= new BrickData();
				_entities[i] = entity;
			}
		}
	}
}