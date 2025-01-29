using UnityEngine;

public class AvatarAirState : AvatarBaseState
{

    public override void EnterState(AvatarStateManager avatar)
    {
    }

    public override void UpdateState(AvatarStateManager avatar)
    {
        AvatarMovement(avatar);
    }

    public override void ExitState(AvatarStateManager avatar)
    {
    }

    private void AvatarMovement(AvatarStateManager avatar)
    {
        var movementAvatar = avatar.MoveAction.ReadValue<Vector2>();

        avatar.transform.position += new Vector3(movementAvatar.x, 0, movementAvatar.y).normalized * Time.deltaTime;

        if (movementAvatar.x == 0)
            return;

        var yAxisAngle = movementAvatar.x > 0 ? 0 : 180;
        avatar.transform.rotation = new Quaternion(avatar.transform.rotation.x, yAxisAngle, avatar.transform.rotation.z, avatar.transform.rotation.w);
    }
}
