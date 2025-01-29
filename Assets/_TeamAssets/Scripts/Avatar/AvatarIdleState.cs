using UnityEngine;

public class AvatarIdleState : AvatarGroundedState
{
    private const string AVATAR_IDLE_ANIMATION = "Idle";
    public override void EnterState(AvatarStateManager avatar)
    {
        base.EnterState(avatar);
        if(avatar.AvatarAnimator != null)
            avatar.AvatarAnimator.Play(AVATAR_IDLE_ANIMATION);
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        base.UpdateState(avatar);
        var movementAvatar =  avatar.MoveAction.ReadValue<Vector2>();
        if (movementAvatar != Vector2.zero)
            avatar.SwitchState(avatar.WalkState);
    }
}
