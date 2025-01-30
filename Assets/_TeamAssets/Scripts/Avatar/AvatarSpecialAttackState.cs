using UnityEngine;

public class AvatarSpecialAttackState : AvatarBaseState
{
    private const string AVATAR_JUMP_ATTACK_ANIMATION = "Special Attack";
    private const float TIMER_ATTACK_COLLIDER_ENABLE_NORMALIZED = 0.3f;
    private const float TIMER_ATTACK_COLLIDER_DISABLE_NORMALIZED = 0.45f;

    public override void EnterState(AvatarStateManager avatar)
    {
        avatar.CanUseSpecialAttack = false;
        avatar.ResetSpecialPoint();
        avatar.AvatarAnimator?.Play(AVATAR_JUMP_ATTACK_ANIMATION);
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        if (!avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).IsName(AVATAR_JUMP_ATTACK_ANIMATION))
            return;

        FinishSpecialAnimation(avatar);
        SwitchAttackDetect(avatar);
    }

    public override void ExitState(AvatarStateManager avatar)
    {
    }

    private void SwitchAttackDetect(AvatarStateManager avatar)
    {
        if (avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= TIMER_ATTACK_COLLIDER_DISABLE_NORMALIZED)
        {
            avatar.AvatarSpecialCollider.enabled = false;
        }
        else
        {
            if (avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= TIMER_ATTACK_COLLIDER_ENABLE_NORMALIZED)
            {
                avatar.AvatarSpecialCollider.enabled = true;
            }
        }
    }

    private void FinishSpecialAnimation(AvatarStateManager avatar)
    {
        if (avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            avatar.SwitchState(avatar.IdleState);
        }
    }
}
