using Core.Entities;
using Data.Entities;
using Data.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor
{
	public class CellsDrawer
	{
		private readonly Texture2D _cellTexture;
		private readonly Texture2D _highlightTexture;
		private readonly Texture2D _whiteTexture;
		private readonly Texture2D _brickTexture;
		private readonly Texture2D _stoneTexture;

		private readonly Color[] _brickColors;
		private readonly GUIStyle _textStyle;

		public CellsDrawer()
		{
			const string texturesFolder = "Assets/Scripts/Editor/LevelEditor/Textures/";
			_cellTexture = (Texture2D) EditorGUIUtility.Load(texturesFolder + "CellTexture.png");
			_highlightTexture = (Texture2D) EditorGUIUtility.Load(texturesFolder + "HighlightTexture.png");
			_whiteTexture = (Texture2D) EditorGUIUtility.Load(texturesFolder + "WhiteTexture.png");
			_brickTexture = (Texture2D) EditorGUIUtility.Load(texturesFolder + "BrickTexture.png");
			_stoneTexture = (Texture2D) EditorGUIUtility.Load(texturesFolder + "StoneTexture.png");

			_brickColors = new[]
			{
				new Color(1f, 0.68f, 0.54f),
				new Color(0.61f, 0.83f, 1f),
				new Color(0.91f, 0.65f, 1f),
				new Color(0.4f, 1f, 0.73f),
			};
			_textStyle = new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.MiddleCenter,
				wordWrap = true,
				fontStyle = FontStyle.Bold,
				active = {textColor = Color.white}
			};
		}

		public void DrawCell(Rect rect)
		{
			if (!ShouldDraw()) return;
			DrawTexture(rect, _cellTexture, new Color(0.18f, 0.18f, 0.18f));
		}

		public void DrawPaddingCell(Rect rect)
		{
			if (!ShouldDraw()) return;
			DrawTexture(rect, _cellTexture, new Color(0.15f, 0.15f, 0.15f));
		}

		public void DrawSelection(Rect rect)
		{
			if (!ShouldDraw()) return;
			DrawTexture(rect, _highlightTexture, new Color(0f, 0.89f, 1f, 0.5f));
		}

		public void DrawHighlight(Rect rect)
		{
			if (!ShouldDraw()) return;
			DrawTexture(rect, _highlightTexture, new Color(1f, 1f, 1f, 0.5f));
		}

		public void DrawEntity(Rect rect, EntityDescription entity)
		{
			if (!ShouldDraw()) return;
			switch (entity.Data)
			{
				case BrickData brick: DrawBrick(rect, brick); break;
				case Stone.StoneData: DrawStone(rect); break;
				case null: break;
				default: DrawGenericEntity(rect, entity); break;
			}
		}

		private void DrawBrick(Rect rect, BrickData brick)
		{
			int group = brick.Group;
			Color color = _brickColors[group % _brickColors.Length];
			DrawTexture(rect, _brickTexture, color);
			DrawText(rect, group.ToString(), Color.white, true);
		}

		private void DrawStone(Rect rect) => DrawTexture(rect, _stoneTexture, new Color(0.49f, 0.44f, 0.41f));

		private void DrawGenericEntity(Rect rect, EntityDescription entity)
		{
			DrawTexture(rect, _whiteTexture, Color.gray);
			DrawText(rect, entity.Data?.GetType().Name, Color.black, true);
		}

		private static bool ShouldDraw() => Event.current.type is EventType.Layout or EventType.Repaint;

		private static void DrawTexture(Rect rect, Texture2D texture, Color color) =>
			GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 1, color, 0, 0);

		private void DrawText(Rect rect, string text, Color color) => DrawText(rect, text, color, false);
		private void DrawText(Rect rect, string text, Color color, bool scaleFont)
		{
			int fontSize = (int) Mathf.Min(rect.size.y * 0.5f, 30);
			DrawText(rect, text, color, scaleFont ? fontSize : _textStyle.fontSize);
		}

		private void DrawText(Rect rect, string text, Color color, int fontSize)
		{
			_textStyle.normal.textColor = color;
			_textStyle.fontSize = fontSize;
			EditorGUI.LabelField(rect, text, _textStyle);
		}
	}
}