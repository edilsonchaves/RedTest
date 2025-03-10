using UnityEngine;

public class AvatarWalkState : AvatarGroundedState
{
    private const string AVATAR_WALK_ANIMATION = "Walk";

    public override void EnterState(AvatarStateManager avatar)
    {
        base.EnterState(avatar);
        if (avatar.AvatarAnimator != null)
            avatar.AvatarAnimator?.Play(AVATAR_WALK_ANIMATION);
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        base.UpdateState(avatar);
        AvatarMovement(avatar);
    }

    private void AvatarMovement(AvatarStateManager avatar)
    {
        var movementAvatar = avatar.MoveAction.ReadValue<Vector2>();
        if (movementAvatar == Vector2.zero)
        {
            avatar.SwitchState(avatar.IdleState);
            return;
        }
        avatar.transform.position += new Vector3(movementAvatar.x, 0, movementAvatar.y).normalized * Time.deltaTime;

        if (movementAvatar.x == 0)
            return;

        var yAxisAngle = movementAvatar.x > 0 ? 0 : 180;
        avatar.transform.rotation = new Quaternion(avatar.transform.rotation.x, yAxisAngle, avatar.transform.rotation.z, avatar.transform.rotation.w);
    }
}
