using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarGroundedState : AvatarBaseState
{
    private AvatarStateManager _avatar;

    public override void EnterState(AvatarStateManager avatar)
    {
        _avatar = avatar;
        avatar.JumpAction.performed += JumpCommand;
        avatar.AttackAction.performed += AttackCommand;
        avatar.SpecialAttackAction.performed += SpecialAttackCommand;
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
    }

    public override void ExitState(AvatarStateManager avatar)
    {
        _avatar = null;
        avatar.JumpAction.performed -= JumpCommand;
        avatar.AttackAction.performed -= AttackCommand;
        avatar.SpecialAttackAction.performed -= SpecialAttackCommand;
    }

    private void JumpCommand(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _avatar.SwitchState(_avatar.JumpState);
        }
    }

    private void AttackCommand(InputAction.CallbackContext context)
    {
        AttackExecution(context);

    }

    private void SpecialAttackCommand(InputAction.CallbackContext context)
    {
        SpecialAttackExecution(context);
    }

    protected virtual void AttackExecution(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _avatar.SwitchState(_avatar.AttackState);
        }
    }

    private void SpecialAttackExecution(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _avatar.SwitchState(_avatar.SpecialAttackState);
        }
    }
}