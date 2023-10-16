using Data.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor
{
	public class LevelEditorData
	{
		private readonly LevelEditorWindow _window;

		// ReSharper disable once InconsistentNaming
		private SerializedObject SO => _window.SO;
		private Vector2Int Size => _window.Size;
		private int LevelPadding => _window.LevelPadding;
		private EntityDescription CurrentEntity
		{
			get => _window.CurrentEntity;
			set => _window.CurrentEntity = value;
		}

		public LevelEditorData(LevelEditorWindow window) => _window = window;

		public void Draw(Rect rect)
		{
			LevelEditorUtils.DrawBox(rect, LevelEditorWindow.BackgroundColor, Color.black, 2);
			rect = LevelEditorUtils.ApplyPadding(rect, _window.Padding);
			using GUILayout.AreaScope areaScope = new(rect);
			
			SO.Update();

			EditorGUILayout.PropertyField(SO.FindProperty(nameof(Size)));
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
			EditorGUILayout.PropertyField(SO.FindProperty(nameof(LevelPadding)));
			
			if (SO.hasModifiedProperties) _window.Dirty();
			
			EditorGUILayout.PropertyField(SO.FindProperty(nameof(CurrentEntity)), true);

			SO.ApplyModifiedProperties();
			
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Save")) _window.SaveChanges();
		}
	}
}