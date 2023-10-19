using Core.Entities;
using Core.Services.NormalizationRules;
using JetBrains.Annotations;
using UnityEngine;

namespace Data.Entities
{
	[CreateAssetMenu(menuName = SOConstants.Configs + "Entity Config")]
	public class EntityConfig : ScriptableObject
	{
		[SerializeField] private Entity _prefab;
		[SerializeField] private bool _isRequiredForLevelCompletion;
		[SerializeReference] [SubclassSelector] private NormalizationRule _normalizationRule;

#if UNITY_EDITOR
		[SerializeField] [UsedImplicitly] private string _typeHint;
#endif

		public Entity Prefab => _prefab;
		public bool IsRequiredForLevelCompletion => _isRequiredForLevelCompletion;
		// ReSharper disable once ConvertToAutoProperty
		public NormalizationRule NormalizationRule => _normalizationRule;

#if UNITY_EDITOR
		private void OnValidate() => _typeHint = _prefab == null ? "" : _prefab.GetDataType().Name;
#endif
	}
}