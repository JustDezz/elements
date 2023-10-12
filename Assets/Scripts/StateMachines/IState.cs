namespace StateMachines
{
	public interface IState
	{
		public void OnEnter();
		public void OnExit();
	}

	public interface IPayloadState<in TPayload> : IState
	{
		public void SetPayload(TPayload payload);
	}
}