public abstract class DummyBaseState
{
    public abstract void EnterState(DummyStateManager dummy);

    public abstract void UpdateState(DummyStateManager dummy);

    public abstract void ExitState(DummyStateManager dummy);
}
