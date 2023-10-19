using UnityEngine;

namespace Core.Levels
{
	public class CellGrid
	{
		public Vector2 Origin { get; }
		public Vector2 CellSize { get; }
		public Vector2 CellGap { get; }

		public CellGrid(Vector2 origin, Vector2 cellSize, Vector2 cellGap)
		{
			Origin = origin;
			CellSize = cellSize;
			CellGap = cellGap;
		}

		public Vector3 ToWorld(Vector2Int index) => ToWorld(index, new Vector2(0.5f, 0.5f));
		public Vector3 ToWorld(Vector2Int index, Vector2 pivot)
		{
			Vector2 gridPosition = (CellSize + CellGap) * index;
			Vector2 pivotOffset = CellSize * pivot;
			return Origin + gridPosition + pivotOffset;	
		}

		public Vector2Int ToCell(Vector2 position)
		{
			Vector2 cellSpace = position - Origin;
			Vector2 index = cellSpace / (CellSize + CellGap);
			return Vector2Int.FloorToInt(index);
		}
	}
}