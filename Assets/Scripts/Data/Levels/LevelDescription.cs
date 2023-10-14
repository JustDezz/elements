using Data.Levels.Visuals;
using UnityEngine;

namespace Data.Levels
{
	[CreateAssetMenu(menuName = SOConstants.Levels + "New Level")]
	public class LevelDescription : ScriptableObject
	{
		[SerializeField] private VisualSettings _visuals;
		
		[SerializeField] [Min(1)] private Vector2Int _size;
		[SerializeField] private EntityDescription[] _entities;

		public VisualSettings Visuals => _visuals;

		public Vector2Int Size
		{
			get => _size;
			set => _size = value;
		}

		public EntityDescription[] Entities
		{
			get => _entities;
			set => _entities = value;
		}
	}
}