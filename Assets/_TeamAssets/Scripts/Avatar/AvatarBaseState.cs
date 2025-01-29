using UnityEngine;

public abstract class AvatarBaseState
{

    public abstract void EnterState(AvatarStateManager avatar);

    public abstract void UpdateState(AvatarStateManager avatar);

    public abstract void ExitState(AvatarStateManager avatar);
}
