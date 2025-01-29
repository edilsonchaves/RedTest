using UnityEngine;

public class AvatarJumpAttackState : AvatarAirState
{
    private const string AVATAR_JUMP_ATTACK_ANIMATION = "Jumping Punch";

    public override void EnterState(AvatarStateManager avatar)
    {
        base.EnterState(avatar);
        avatar.AvatarAnimator?.Play(AVATAR_JUMP_ATTACK_ANIMATION);
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        base.UpdateState(avatar);
    }

    public override void ExitState(AvatarStateManager avatar)
    {
    }
}
