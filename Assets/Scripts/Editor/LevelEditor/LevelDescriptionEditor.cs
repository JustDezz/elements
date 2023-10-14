using Data.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor
{
	[CustomEditor(typeof(LevelDescription))]
	public class LevelDescriptionEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Open level editor")) LevelEditorWindow.Open((LevelDescription) target);
			DrawDefaultInspector();
		}
	}
}