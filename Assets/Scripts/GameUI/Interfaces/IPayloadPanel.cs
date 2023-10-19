namespace GameUI.Interfaces
{
	public interface IPayloadPanel<in T>
	{
		public void Init(T payload);
	}
}