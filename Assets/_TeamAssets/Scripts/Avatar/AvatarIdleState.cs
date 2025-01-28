using UnityEngine;

public class AvatarIdleState : AvatarBaseState
{
    private const string AVATAR_IDLE_ANIMATION = "Idle";

    public override void EnterState(AvatarStateManager avatar)
    {
        if(avatar.AvatarAnimator != null)
            avatar.AvatarAnimator.Play(AVATAR_IDLE_ANIMATION);
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        var movementAvatar =  avatar.MoveAction.ReadValue<Vector2>();
        if (movementAvatar != Vector2.zero)
            avatar.SwitchState(avatar.WalkState);
    }
}
