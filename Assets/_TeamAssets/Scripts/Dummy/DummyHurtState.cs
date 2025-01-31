using System.Collections;
using UnityEngine;

public class DummyHurtState : DummyBaseState
{
    private const string COROUTINE_METHOD_NAME = "HurtCoroutine";
    public override void EnterState(DummyStateManager dummy)
    {
        dummy.StartAnimation(COROUTINE_METHOD_NAME);
    }

    public override void ExitState(DummyStateManager dummy)
    {
    }

    public override void UpdateState(DummyStateManager dummy)
    {

    }
}
