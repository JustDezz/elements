using GameUI.Interfaces;
using GameUI.Panels;

namespace GameUI
{
	public interface IUIManager
	{
		public TPanel Open<TPanel>() where TPanel : Panel;
		public TPanel Open<TPanel, TPayload>(TPayload payload) where TPanel : Panel, IPayloadPanel<TPayload>;
		public TPanel TryGet<TPanel>() where TPanel : Panel;
		public bool TryGet<TPanel>(out TPanel panel) where TPanel : Panel;
		public void Close<TPanel>() where TPanel : Panel;
	}
}