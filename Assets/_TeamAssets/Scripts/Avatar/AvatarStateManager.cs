using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarStateManager : MonoBehaviour
{
    private AvatarBaseState _currentState;
    private AvatarIdleState _idleState = new AvatarIdleState();
    public AvatarIdleState IdleState => _idleState;
    private AvatarWalkState _walkState = new AvatarWalkState();
    public AvatarWalkState WalkState => _walkState;

    private AvatarJumpState _jumpState = new AvatarJumpState();
    public AvatarJumpState JumpState => _jumpState;

    private AvatarJumpAttackState _jumpAttackState = new AvatarJumpAttackState();
    public AvatarJumpAttackState JumpAttackState => _jumpAttackState;

    private InputAction _moveAction;
    public InputAction MoveAction => _moveAction;

    private InputAction _jumpAction;
    public InputAction JumpAction => _jumpAction;

    private InputAction _attackAction;
    public InputAction AttackAction => _attackAction;

    [SerializeField] private Rigidbody _rbAvatar;
    public Rigidbody RbAvatar => _rbAvatar;

    [SerializeField] private PlayerInput _avatarInput;
    [SerializeField] private Animator _avatarAnimator;
    public Animator AvatarAnimator => _avatarAnimator;

    private const string MOVEMENT_INPUT_ACTION = "Move";
    private const string JUMP_INPUT_ACTION = "Jump";
    private const string ATTACK_INPUT_ACTION = "Attack";
    private const string GROUND_TAG = "Ground";

    private void Start()
    {
        _currentState = _idleState;
        _moveAction = _avatarInput.actions.FindAction(MOVEMENT_INPUT_ACTION);
        _jumpAction = _avatarInput.actions.FindAction(JUMP_INPUT_ACTION);
        _attackAction = _avatarInput.actions.FindAction(ATTACK_INPUT_ACTION);

        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(AvatarBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            SwitchState(IdleState);
        }
    }
}
