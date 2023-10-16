using System.Collections.Generic;
using System.Linq;
using Data.Levels;
using Tools.Extensions;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor
{
	public class LevelEditorWindow : EditorWindow
	{
		[SerializeField] [Min(1)] internal Vector2Int Size;
		[SerializeField] [Min(0)] internal int LevelPadding;
		[SerializeReference] internal List<EntityDescription> Entities;
		[SerializeReference] internal EntityDescription CurrentEntity;
		
		// ReSharper disable once InconsistentNaming
		internal SerializedObject SO;
		internal LevelDescription Level;
		
		private Vector2 _gridArea;
		private Vector2 _editArea;
		private Vector2 _targetSize;
		
		private LevelEditorGrid _grid;
		private LevelEditorData _data;

		public static Color BackgroundColor => EditorGUIUtility.isProSkin
			? new Color32(56, 56, 56, 255)
			: new Color32(194, 194, 194, 255);
		public float Margin { get; set; }
		public float Padding { get; set; }
		public float Spacing { get; set; }

		public static void Open(LevelDescription level)
		{
			LevelEditorWindow window = GetWindow<LevelEditorWindow>(false, level.name);

			Vector2 gridArea = new(500, 500);
			Vector2 editArea = new(200, 500);
			const float margin = 3;
			const float padding = 2;
			const float spacing = 5;

			window._gridArea = gridArea;
			window._editArea = editArea;
			window.Margin = margin;
			window.Padding = padding;
			window.Spacing = spacing;
			window._targetSize = new Vector2(
				margin + gridArea.x + spacing + editArea.x + margin,
				margin + gridArea.y + margin);
			
			window.Level = level;

			window.Show();
		}

		private void Validate()
		{
			if (SO != null) return;
			
			Size = Level.Size;
			LevelPadding = Level.Padding;
			Entities = Level.Entities.Select(e => new EntityDescription(e)).ToList();
			CurrentEntity = new EntityDescription();
			_grid = new LevelEditorGrid(this);
			_data = new LevelEditorData(this);
			SO = new SerializedObject(this);
		}

		private void Update() => Repaint();

		private void OnGUI()
		{
			Validate();
			
			Vector2 emptySpace = new(Margin * 2 + Spacing, Margin * 2);
			Vector2 currentSize = position.size - emptySpace;
			Vector2 scalar = currentSize / (_targetSize - emptySpace);

			Rect gridRect = new(Margin, Margin, _gridArea.x * scalar.x, _gridArea.y * scalar.y);
			Rect editRect = new(gridRect.xMax + Spacing, Margin, _editArea.x * scalar.x, _editArea.y * scalar.y);

			_grid.Draw(gridRect);
			_data.Draw(editRect);
		}

		public void Dirty() => hasUnsavedChanges = true;
		public override void SaveChanges()
		{
			Save();
			base.SaveChanges();
		}

		private void Save()
		{
			EntityDescription[] savedEntities = Entities
				.Where(e =>
					e.Data != null &&
					e.Position.x >= 0 && e.Position.x < Size.x &&
					e.Position.y >= 0 && e.Position.y < Size.y)
				.Select(e => new EntityDescription(e))
				.OrderBy(e => Size.PositionToIndex(e.Position))
				.ToArray();

			Vector2Int size = Vector2Int.zero;
			foreach (EntityDescription entity in savedEntities) size = Vector2Int.Max(size, entity.Position);
			Size = size + Vector2Int.one;

			Level.Size = Size;
			Level.Padding = LevelPadding;
			Level.Entities = savedEntities;

			EditorUtility.SetDirty(Level);
			AssetDatabase.SaveAssetIfDirty(Level);
		}
	}
}