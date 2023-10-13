using UnityEngine;

namespace Data.Levels.Visuals
{
	[CreateAssetMenu(menuName = SOConstants.Levels + "VisualsConfig")]
	public class VisualSettings : ScriptableObject
	{
		[SerializeField] private GameObject[] _brickViews;

		public GameObject[] BrickViews => _brickViews;
	}
}