using UnityEngine;

namespace Core.Levels
{
	public class CellGrid
	{
		private readonly Vector2 _origin;
		private readonly Vector2 _cellSize;
		private readonly Vector2 _cellGap;

		public CellGrid(Vector2 origin, Vector2 cellSize, Vector2 cellGap)
		{
			_origin = origin;
			_cellSize = cellSize;
			_cellGap = cellGap;
		}

		public Vector3 ToWorld(Vector2Int index) => ToWorld(index, new Vector2(0.5f, 0.5f));
		public Vector3 ToWorld(Vector2Int index, Vector2 pivot)
		{
			Vector2 gridPosition = (_cellSize + _cellGap) * index;
			Vector2 pivotOffset = _cellSize * pivot;
			return _origin + gridPosition + pivotOffset;	
		}

		public Vector2Int ToCell(Vector2 position)
		{
			Vector2 cellSpace = position - _origin;
			Vector2 index = cellSpace / (_cellSize + _cellGap);
			return Vector2Int.FloorToInt(index);
		}
	}
}