using Core.Environment;
using UnityEngine;

namespace Data.Levels.Visuals
{
	[CreateAssetMenu(menuName = SOConstants.Levels + "VisualsConfig")]
	public class VisualSettings : ScriptableObject
	{
		[SerializeField] private GameObject[] _brickViews;
		[SerializeField] private LevelEnvironment _environment;

		public GameObject[] BrickViews => _brickViews;
		public LevelEnvironment Environment => _environment;
	}
}