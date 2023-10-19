using Data;
using GameUI.Panels;
using UnityEngine;

namespace GameUI
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "UI Config")]
	public class UIConfig : ScriptableObject
	{
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Panel[] _panels;

		public Canvas Canvas => _canvas;
		public Panel[] Panels => _panels;
	}
}