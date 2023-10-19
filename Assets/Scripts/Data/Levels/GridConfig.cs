using UnityEngine;

namespace Data.Levels
{
	[CreateAssetMenu(menuName = SOConstants.Levels + "Grid Config")]
	public class GridConfig : ScriptableObject
	{
		[SerializeField] private Vector2 _origin;
		[SerializeField] private Vector2 _cellSize = Vector2.one;
		[SerializeField] private Vector2 _cellGap;

		public Vector2 Origin => _origin;
		public Vector2 CellSize => _cellSize;
		public Vector2 CellGap => _cellGap;
	}
}