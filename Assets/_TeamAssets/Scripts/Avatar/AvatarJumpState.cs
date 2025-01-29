using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarJumpState : AvatarAirState
{
    private AvatarStateManager _avatar;
    private bool _isJumpingDown = false;
    private Vector2 _jumpForce = new Vector2(0, 400);
    private const string AVATAR_JUMPING_UP_ANIMATION = "Jumping Up";
    private const string AVATAR_JUMPING_DOWN_ANIMATION = "Jumping Down";
    public override void EnterState(AvatarStateManager avatar)
    {
        base.EnterState(avatar);
        _avatar = avatar;
        _isJumpingDown = false;
        avatar.AttackAction.performed += Attack;

        avatar.RbAvatar.AddForce(_jumpForce);
        avatar.AvatarAnimator?.Play(AVATAR_JUMPING_UP_ANIMATION);

    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        base.UpdateState(avatar);

        if(avatar.RbAvatar.linearVelocity.y < 0 && !_isJumpingDown)
        {
            _isJumpingDown = true;
            avatar.AvatarAnimator?.Play(AVATAR_JUMPING_DOWN_ANIMATION);
        }
    }

    public override void ExitState(AvatarStateManager avatar)
    {
        _avatar = null;
        avatar.AttackAction.performed -= Attack;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _avatar.SwitchState(_avatar.JumpAttackState);
        }
    }
}
