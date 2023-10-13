using Data.Entities;
using Data.Levels.Visuals;
using UnityEngine;

namespace Data.Levels
{
	[CreateAssetMenu(menuName = SOConstants.Levels + "New Level")]
	public class LevelDescription : ScriptableObject
	{
		[SerializeField] private EntityDescription[] _entities;
		[SerializeField] private VisualSettings _visuals;

		public EntityDescription[] Entities => _entities;
		public VisualSettings Visuals => _visuals;

		private void OnValidate()
		{
			for (int i = 0; i < _entities.Length; i++)
			{
				EntityDescription entity = _entities[i];
				entity.Data ??= new BrickData();
				_entities[i] = entity;
			}
		}
	}
}