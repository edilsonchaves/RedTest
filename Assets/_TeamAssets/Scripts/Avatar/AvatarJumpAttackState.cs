using UnityEngine;

public class AvatarJumpAttackState : AvatarAirState
{
    private const string AVATAR_JUMP_ATTACK_ANIMATION = "Jumping Punch";
    private const int AVATAR_ATTACK_SOUND_INDEX = 0;
    private const float TIMER_ATTACK_COLLIDER_ENABLE_NORMALIZED = 0.3f;
    private const float TIMER_ATTACK_COLLIDER_DISABLE_NORMALIZED = 0.45f;

    public override void EnterState(AvatarStateManager avatar)
    {
        base.EnterState(avatar);
        avatar.AvatarAnimator?.Play(AVATAR_JUMP_ATTACK_ANIMATION);
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        base.UpdateState(avatar);
        SwitchAttackDetect(avatar);
    }

    public override void ExitState(AvatarStateManager avatar)
    {
    }

    private void SwitchAttackDetect(AvatarStateManager avatar)
    {
        if (avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= TIMER_ATTACK_COLLIDER_DISABLE_NORMALIZED)
        {
            avatar.AvatarPunchCollider.enabled = false;
        }
        else
        {
            if (avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= TIMER_ATTACK_COLLIDER_ENABLE_NORMALIZED)
            {
                avatar.AvatarPunchCollider.enabled = true;
                avatar.ExecuteMusic(AVATAR_ATTACK_SOUND_INDEX);
            }
        }
    }
}
