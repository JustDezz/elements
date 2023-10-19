using UnityEngine;
using Zenject;

namespace GameUI.Panels
{
	public abstract class Panel : MonoBehaviour
	{
		protected DiContainer Container { get; private set; }
		protected IUIManager Manager { get; private set; }

		[Inject]
		public void Construct(DiContainer container, IUIManager uiManager)
		{
			Container = container;
			Manager = uiManager;
		}

		public abstract void Close();
	}
}