using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarAttackState : AvatarGroundedState
{
    private AvatarStateManager _avatar;
    private string _currentAttackCombo;
    private int _attackIndex = 1;
    private int _indexLimit = 3;
    private bool _isBlockCombo;
    private bool _isExecutingAttack;
    private const string AVATAR_ATTACK_ANIMATION = "Punch ";
    private const float TIMER_ATTACK_COLLIDER_ENABLE_NORMALIZED = 0.5f;
    private const float TIMER_ATTACK_COLLIDER_DISABLE_NORMALIZED = 0.75f;

    public override void EnterState(AvatarStateManager avatar)
    {
        base.EnterState(avatar);
        ResetAttackIndex();
        _avatar = avatar;
        ExecuteAttackAnimation();
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        if (!avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).IsName(_currentAttackCombo))
            return;

        FinishPunchAnimation(avatar);
        SwitchAttackDetect(avatar);
    }

    private void FinishPunchAnimation(AvatarStateManager avatar)
    {
        if (avatar.AvatarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (_isBlockCombo)
            {
                _isBlockCombo = false;
                ExecuteAttackAnimation();
            }
            else
            {
                _isBlockCombo = false;
                _isExecutingAttack = false;
                avatar.SwitchState(avatar.IdleState);
            }
        }
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
            }
        }
    }

    public override void ExitState(AvatarStateManager avatar)
    {
        base.ExitState(avatar);
        _avatar = null;
    }
    protected override void AttackExecution(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isExecutingAttack)
            {
                _isBlockCombo = true;
            }
        }
    }

    private void ExecuteAttackAnimation()
    {
        _isExecutingAttack = true;
        _currentAttackCombo = AVATAR_ATTACK_ANIMATION + _attackIndex;
        _avatar.AvatarAnimator?.Play(_currentAttackCombo);
        IncreaseAttackIndex();
    }
    private void IncreaseAttackIndex()
    {
        if (_attackIndex < _indexLimit)
        {
            _attackIndex++;
        }
        else
        {
            _attackIndex = 1;
        }
    }

    protected void ResetAttackIndex()
    {
        _attackIndex = 1;
    }
}
