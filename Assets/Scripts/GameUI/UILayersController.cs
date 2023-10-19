using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
	public class UILayersController
	{
		private readonly Canvas _canvas;
		private readonly Dictionary<int, RectTransform> _layers;

		public UILayersController(Canvas canvas)
		{
			_canvas = canvas;
			_layers = new Dictionary<int, RectTransform>();
		}
		
		public RectTransform GetLayer(int index) => _layers.TryGetValue(index, out RectTransform transform) ? transform : AddLayer(index);

		private RectTransform AddLayer(int index)
		{
			GameObject layer = new("Layer " + index);
			Canvas canvas = layer.AddComponent<Canvas>();
			layer.AddComponent<GraphicRaycaster>();
			canvas.overrideSorting = true;
			canvas.sortingOrder = index;
			
			RectTransform transform = (RectTransform) canvas.transform;
			RectTransform parent = (RectTransform) _canvas.transform;
			transform.SetParent(parent, false);
			
			transform.pivot = new Vector2(0.5f, 0.5f);
			transform.anchoredPosition = Vector2.zero;
			transform.anchorMin = new Vector2(0, 0);
			transform.anchorMax = new Vector2(1, 1);
			transform.sizeDelta = Vector2.zero;

			_layers.Add(index, transform);
			return transform;
		}
	}
}