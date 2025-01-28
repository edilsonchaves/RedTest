using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarStateManager : MonoBehaviour
{
    private AvatarBaseState _currentState;
    private AvatarIdleState _idleState = new AvatarIdleState();
    public AvatarIdleState IdleState => _idleState;
    private AvatarWalkState _walkState = new AvatarWalkState();
    public AvatarWalkState WalkState => _walkState;

    private InputAction _moveAction;
    public InputAction MoveAction => _moveAction;

    [SerializeField] private PlayerInput _avatarInput;
    [SerializeField] private Animator _avatarAnimator;
    public Animator AvatarAnimator => _avatarAnimator;

    private const string MOVEMENT_INPUT_ACTION = "Move";

    private void Start()
    {
        _currentState = _idleState;
        _moveAction = _avatarInput.actions.FindAction(MOVEMENT_INPUT_ACTION);
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(AvatarBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
