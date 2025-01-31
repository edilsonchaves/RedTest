using System.Collections;
using UnityEngine;

public class DummyHurtState : DummyBaseState
{
    private const string COROUTINE_METHOD_NAME = "HurtCoroutine";
    private const int DUMMY_HURT_SOUND_INDEX = 0;

    public override void EnterState(DummyStateManager dummy)
    {
        dummy.StartAnimation(COROUTINE_METHOD_NAME);
        dummy.ExecuteMusic(DUMMY_HURT_SOUND_INDEX);
    }

    public override void ExitState(DummyStateManager dummy)
    {
    }

    public override void UpdateState(DummyStateManager dummy)
    {

    }
}
