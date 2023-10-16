using Data.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor
{
	public class LevelEditorGrid
	{
		private readonly LevelEditorWindow _window;
		private readonly CellsDrawer _cellsDrawer;

		// ReSharper disable once InconsistentNaming
		private SerializedObject SO => _window.SO;
		private Vector2Int Size => _window.Size;
		private int LevelPadding => _window.LevelPadding;
		private EntityDescription CurrentEntity
		{
			get => _window.CurrentEntity;
			set => _window.CurrentEntity = value;
		}

		public LevelEditorGrid(LevelEditorWindow window)
		{
			_window = window;
			_cellsDrawer = new CellsDrawer();
		}

		public void Draw(Rect rect)
		{
			LevelEditorUtils.DrawBox(rect, LevelEditorWindow.BackgroundColor, Color.black, 2);
			rect = LevelEditorUtils.ApplyPadding(rect, _window.Padding);
			using GUILayout.AreaScope areaScope = new(rect);

			Vector2Int size = Size + new Vector2Int(LevelPadding * 2, 0);
			Vector2 maxCellSize = rect.size / size;
			Vector2 cellSize = Vector2.one * Mathf.Min(maxCellSize.x, maxCellSize.y);
			Vector2 origin = new((rect.width - size.x * cellSize.x) / 2, rect.height - cellSize.y);

			SerializedProperty array = SO.FindProperty(nameof(_window.Entities));

			SO.Update();
			for (int i = 0; i < Size.x; i++)
			for (int j = 0; j < Size.y; j++)
			{
				Vector2Int cell = new(i, j);
				Vector2 cellPosition = origin + cellSize * new Vector2(i + LevelPadding, -j);
				Rect cellRect = new(cellPosition, cellSize);
				_cellsDrawer.DrawCell(cellRect);

				ProcessCell(cell, cellRect, array);
			}

			for (int i = 0; i < LevelPadding; i++)
			for (int j = 0; j < size.y; j++)
			{
				Vector2 cellPosition = origin + cellSize * new Vector2(i, -j);
				Rect cellRect = new(cellPosition, cellSize);
				_cellsDrawer.DrawPaddingCell(cellRect);
				cellRect.position += new Vector2(cellSize.x * (Size.x + LevelPadding), 0);
				_cellsDrawer.DrawPaddingCell(cellRect);
			}

			if (SO.hasModifiedProperties) _window.Dirty();
			SO.ApplyModifiedProperties();
		}

		private void ProcessCell(Vector2Int cell, Rect cellRect, SerializedProperty array)
		{
			int index = -1;
			EntityDescription entity = null; 
			for (int i = 0; i < array.arraySize; i++)
			{
				SerializedProperty element = array.GetArrayElementAtIndex(i);
				EntityDescription entityAtIndex = (EntityDescription) element.managedReferenceValue;
				if (entityAtIndex == null) continue;
				if (entityAtIndex.Position != cell) continue;
				index = i;
				entity = entityAtIndex;
				break;
			}
			
			if (entity != null)
			{
				_cellsDrawer.DrawEntity(cellRect, entity);
				if (entity == CurrentEntity) _cellsDrawer.DrawSelection(cellRect);
			}

			if (!cellRect.Contains(Event.current.mousePosition)) return;
			switch (Event.current)
			{
				case {type: EventType.MouseDown or EventType.MouseDrag, button: 0}:
				{
					if (CurrentEntity == null) DeleteEntity(array, index);
					SetEntity(CurrentEntity, array, index, cell);
					break;
				}
				case {type: EventType.MouseDown or EventType.MouseDrag, button: 1}:
				{
					DeleteEntity(array, index);
					break;
				}
				case {type: EventType.MouseDown, button: 2}:
				{
					CurrentEntity = new EntityDescription(entity);
					break;
				}
			}

			_cellsDrawer.DrawHighlight(cellRect);
		}

		private static void SetEntity(EntityDescription entity, SerializedProperty array, int index, Vector2Int cell)
		{
			if (index == -1)
			{
				index = array.arraySize;
				array.arraySize++;
			}

			SerializedProperty element = array.GetArrayElementAtIndex(index);
			element.managedReferenceValue = new EntityDescription(entity, cell);
		}

		private static void DeleteEntity(SerializedProperty array, int index)
		{
			if (index == -1) return;
			array.DeleteArrayElementAtIndex(index);
		}
	}
}