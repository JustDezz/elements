namespace StateMachines
{
	public abstract class State : IState
	{
		public StateMachine StateMachine { get; }

		public State(StateMachine stateMachine) => StateMachine = stateMachine;

		public abstract void OnEnter();
		public abstract void OnExit();
	}
	
	public abstract class PayloadState<TPayload> : State, IPayloadState<TPayload>
	{
		protected PayloadState(StateMachine stateMachine) : base(stateMachine) { }

		public abstract void SetPayload(TPayload payload);
	}
}