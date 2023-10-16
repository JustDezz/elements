using System;
using System.Threading;
using Core.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Services
{
	public class LevelPlayer
	{
		private readonly IInstantiator _instantiator;

		public LevelPlayer(IInstantiator instantiator) => _instantiator = instantiator;

		public async UniTask Play(Level level, CancellationToken ct)
		{
			object[] extraArgs = {level};
			MoveMaker moveMaker = _instantiator.Instantiate<MoveMaker>(extraArgs);
			EntityMover mover = _instantiator.Instantiate<EntityMover>(extraArgs);
			WinCondition winCondition = _instantiator.Instantiate<WinCondition>(extraArgs);
			FieldNormalizer normalizer = _instantiator.Instantiate<FieldNormalizer>(new object[] {level, mover});
			while (!ct.IsCancellationRequested)
			{
				float moveDuration = 0;
				foreach (Move move in await moveMaker.MakeMoves(ct))
					moveDuration = Mathf.Max(mover.Move(move), moveDuration);
				await UniTask.Delay(TimeSpan.FromSeconds(moveDuration), cancellationToken: ct);
				await normalizer.Normalize(ct);

				if (winCondition.IsMet()) return;
			}
		}
	}
}