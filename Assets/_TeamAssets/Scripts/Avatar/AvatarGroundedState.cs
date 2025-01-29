using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarGroundedState : AvatarBaseState
{
    private AvatarStateManager _avatar;
    public override void EnterState(AvatarStateManager avatar)
    {
        _avatar = avatar;
        avatar.JumpAction.performed += Jump;
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
    }

    public override void ExitState(AvatarStateManager avatar)
    {
        _avatar = null;
        avatar.JumpAction.performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _avatar.SwitchState(_avatar.JumpState);
        }
    }
}
