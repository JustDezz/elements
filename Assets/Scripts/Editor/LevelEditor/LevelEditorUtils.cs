using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor
{
	public static class LevelEditorUtils
	{
		public static void DrawBox(Rect rect, Color fillColor, Color borderColor, float borderWidth)
		{
			EditorGUI.DrawRect(rect, borderColor);
			Rect fillRect = ApplyPadding(rect, borderWidth);
			EditorGUI.DrawRect(fillRect, fillColor);
		}

		public static Rect ApplyPadding(Rect rect, float padding)
		{
			rect.position += new Vector2(padding, padding);
			rect.size -= new Vector2(padding, padding) * 2;
			return rect;
		}
	}
}