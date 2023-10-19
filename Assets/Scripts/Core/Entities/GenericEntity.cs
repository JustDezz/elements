using System;
using Data.Entities;

namespace Core.Entities
{
	public abstract class GenericEntity<TData> : Entity where TData : EntityData
	{
		public override void SetGenericData(EntityData entityData) => SetData(entityData as TData);
		public override Type GetDataType() => typeof(TData);

		protected abstract void SetData(TData entityData);
	}
}