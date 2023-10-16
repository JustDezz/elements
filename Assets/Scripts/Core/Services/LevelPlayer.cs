using System;
using System.Linq;
using System.Threading;
using Core.Entities;
using Core.Levels;
using Cysharp.Threading.Tasks;
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
			FieldNormalizer normalizer = _instantiator.Instantiate<FieldNormalizer>(new object[] {level, mover});
			while (!ct.IsCancellationRequested)
			{
				Move move = await moveMaker.MakeMove(ct);
				Entity entityAtEnd = level.Entities.FirstOrDefault(e => e.Position == move.EndPosition);
				if (entityAtEnd != null) mover.Move(new Move(entityAtEnd, move.StartPosition));
				float duration = mover.Move(move);
				await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: ct);
				await normalizer.Normalize(ct);
			}
		}
	}
}