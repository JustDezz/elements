using System;
using System.Collections.Generic;
using GameUI.Interfaces;
using GameUI.Panels;
using UnityEngine;

namespace GameUI
{
	public class UIManager : IUIManager
	{
		private readonly IUIFactory _uiFactory;
		private readonly Dictionary<Type, Panel> _openedPanels;

		public UIManager(IUIFactory uiFactory)
		{
			_uiFactory = uiFactory;
			_openedPanels = new Dictionary<Type, Panel>();
		}

		public TPanel Open<TPanel>() where TPanel : Panel
		{
			TPanel panel = CreateOrNull<TPanel>();
			if (panel is IInitializablePanel initializable) initializable.Init();
			return panel;
		}

		public TPanel Open<TPanel, TPayload>(TPayload payload) where TPanel : Panel, IPayloadPanel<TPayload>
		{
			TPanel panel = CreateOrNull<TPanel>();
			panel.Init(payload);
			return panel;
		}
		
		public void Close<TPanel>() where TPanel : Panel => _openedPanels.Remove(typeof(TPanel));

		public TPanel TryGet<TPanel>() where TPanel : Panel
		{
			TryGet(out TPanel panel);
			return panel;
		}

		public bool TryGet<TPanel>(out TPanel panel) where TPanel : Panel
		{
			bool hasValue = _openedPanels.TryGetValue(typeof(TPanel), out Panel p);
			panel = p as TPanel;
			return hasValue;
		}

		private TPanel CreateOrNull<TPanel>() where TPanel : Panel
		{
			Type panelType = typeof(TPanel);
			bool hasKey = _openedPanels.TryGetValue(panelType, out Panel opened);
			if (hasKey && opened != null)
			{
				Debug.LogError($"Panel of type {typeof(TPanel).Name} is already opened. Please consider closing it before opening new one");
				return null;
			}

			TPanel panel = _uiFactory.Create<TPanel>();
			if (hasKey) _openedPanels[panelType] = panel;
			else _openedPanels.Add(panelType, panel);
			return panel;
		}
	}
}