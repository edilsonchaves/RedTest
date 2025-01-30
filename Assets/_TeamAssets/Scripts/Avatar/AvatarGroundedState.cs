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
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
    }

    public override void ExitState(AvatarStateManager avatar)
    {
        _avatar = null;
        avatar.JumpAction.performed -= JumpCommand;
        avatar.AttackAction.performed -= AttackCommand;
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

    protected virtual void AttackExecution(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _avatar.SwitchState(_avatar.AttackState);
        }
    }
}