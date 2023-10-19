using GameUI.Panels;
using UnityEngine;

namespace GameUI
{
	public interface IUIFactory
	{
		public void SpawnRoot();
		public TPanel Create<TPanel>() where TPanel : Panel;
		public GameObject SpawnAtLayer(Object prefab, int layer);
		public void MoveToLayer(Transform element, int layer);
		public T SpawnAtLayer<T>(T prefab, int layer) where T : Object;
	}
}