using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Panels
{
	public class Backdrop : MonoBehaviour
	{
		[SerializeField] private float _defaultAnimationDuration;
		[SerializeField] private Image _image;

		private CancellationTokenSource _cts;
		private CancellationToken _destroyCt;

		private static Backdrop Instance { get; set; }

		private void Awake()
		{
			Instance = this;
			_destroyCt = this.GetCancellationTokenOnDestroy();
		}

		public static UniTask Drop() => Drop(CancellationToken.None);
		public static UniTask Drop(CancellationToken ct) => Drop(Instance._defaultAnimationDuration, ct);
		public static UniTask Drop(float duration) => Drop(duration, CancellationToken.None);
		public static UniTask Drop(float duration, CancellationToken ct) => Instance.Animate(1, duration, ct);
		
		public static UniTask Raise() => Raise(CancellationToken.None);
		public static UniTask Raise(CancellationToken ct) => Raise(Instance._defaultAnimationDuration, ct);
		public static UniTask Raise(float duration) => Raise(duration, CancellationToken.None);
		public static UniTask Raise(float duration, CancellationToken ct) => Instance.Animate(0, duration, ct);

		private UniTask Animate(float to, float duration, CancellationToken ct)
		{
			if (Mathf.Approximately(_image.color.a, to)) return UniTask.CompletedTask;
			
			_cts.CancelAndDispose();
			_cts = CancellationTokenSource.CreateLinkedTokenSource(ct, _destroyCt);

			DOTween.Kill(_image);
			return _image
				.DOFade(to, duration)
				.SetTarget(this)
				.ToUniTask(cancellationToken: _cts.Token, tweenCancelBehaviour: TweenCancelBehaviour.Complete);
		}
	}
}