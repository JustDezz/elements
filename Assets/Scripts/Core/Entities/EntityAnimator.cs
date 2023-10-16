using UnityEngine;

namespace Core.Entities
{
	public class EntityAnimator : MonoBehaviour
	{
		[SerializeField] private Animator _animator;

		public void Play(int stateHash) => Play(stateHash, 0);
		public void Play(int stateHash, float normalizedTime) => _animator.Play(stateHash, 0, normalizedTime);
		public float GetCurrentAnimationDuration() => _animator.GetCurrentAnimatorStateInfo(0).length;
	}
}