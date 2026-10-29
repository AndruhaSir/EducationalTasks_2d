public class StateMachine<TState> where TState : class, IState
{
    public TState CurrentState { get; private set; }

    public void ChangeState(TState newState)
    {
        if (ReferenceEquals(CurrentState, newState))
        {
            return;
        }

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Tick()
    {
        CurrentState?.Tick();
    }

    public void FixedTick()
    {
        if (CurrentState is IFixedTickState fixedTickState)
        {
            fixedTickState.FixedTick();
        }
    }
}
