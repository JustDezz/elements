using UnityEngine;

namespace Data.Levels
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "Levels Config")]
	public class LevelsConfig : ScriptableObject
	{
		[SerializeField] private LevelDescription[] _levels;

		public LevelDescription[] Levels => _levels;
	}
}