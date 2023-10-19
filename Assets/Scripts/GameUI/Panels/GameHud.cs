using System;
using DG.Tweening;
using GameUI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Panels
{
	public class GameHud : Panel, IInitializablePanel
	{
		public event Action RestartClicked;
		public event Action NextClicked;

		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _nextButton;

		public void Init()
		{
			_restartButton.onClick.AddListener(OnRestartClicked);
			_nextButton.onClick.AddListener(OnNextClicked);
		}
		
		private void OnRestartClicked()
		{
			RestartClicked?.Invoke();
			
			SetRestartActive(false);
			DOTween.Kill(_restartButton, true);
			_restartButton.transform
				.DOLocalRotate(Vector3.forward * 360, 0.5f, RotateMode.LocalAxisAdd)
				.SetTarget(_restartButton)
				.OnComplete(() => SetRestartActive(true));
		}

		private void OnNextClicked() => NextClicked?.Invoke();

		public void SetRestartActive(bool isActive) => _restartButton.enabled = isActive;

		public override void Close()
		{
			Manager.Close<GameHud>();
			Destroy(gameObject);
		}
	}
}