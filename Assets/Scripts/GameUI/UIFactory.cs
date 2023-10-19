using System;
using System.Collections.Generic;
using GameUI.Panels;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameUI
{
	public class UIFactory : IUIFactory
	{
		private readonly IInstantiator _instantiator;

		private readonly Dictionary<Type, Panel> _prefabs;
		private readonly UIConfig _config;

		private UILayersController _layers;
		private Canvas _canvas;

		protected UIFactory(IInstantiator instantiator, UIConfig config)
		{
			_config = config;
			_instantiator = instantiator;

			_prefabs = new Dictionary<Type, Panel>();
			foreach (Panel panel in config.Panels)
			{
				if (panel == null) continue;
				_prefabs.Add(panel.GetType(), panel);
			}
		}

		public void SpawnRoot()
		{
			_canvas = _instantiator.InstantiatePrefabForComponent<Canvas>(_config.Canvas);
			_layers = new UILayersController(_canvas);
		}

		public TPanel Create<TPanel>() where TPanel : Panel
		{
			Panel prefab = _prefabs[typeof(TPanel)];
			TPanel panel = _instantiator.InstantiatePrefabForComponent<TPanel>(prefab, _canvas.transform);
			return panel;
		}

		public GameObject SpawnAtLayer(Object prefab, int layer) =>
			_instantiator.InstantiatePrefab(prefab, _layers.GetLayer(layer));

		public T SpawnAtLayer<T>(T prefab, int layer) where T : Object
			=> _instantiator.InstantiatePrefabForComponent<T>(prefab, _layers.GetLayer(layer));

		public void MoveToLayer(Transform element, int layer) =>
			element.transform.SetParent(_layers.GetLayer(layer));
	}
}