using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarStateManager : MonoBehaviour
{
    private bool _canUseSpecialAttack = true;
    public bool CanUseSpecialAttack
    {
        get { return _canUseSpecialAttack; }
        set { _canUseSpecialAttack = value; }
    }

    private float _currentSpecialPoint = 100;
    private float _maxSpecialPoint = 100;
    private AvatarBaseState _currentState;
    private AvatarIdleState _idleState = new AvatarIdleState();
    public AvatarIdleState IdleState => _idleState;
    private AvatarWalkState _walkState = new AvatarWalkState();
    public AvatarWalkState WalkState => _walkState;

    private AvatarJumpState _jumpState = new AvatarJumpState();
    public AvatarJumpState JumpState => _jumpState;

    private AvatarAttackState _attackState = new AvatarAttackState();
    public AvatarAttackState AttackState => _attackState;

    private AvatarSpecialAttackState _specialAttackState = new AvatarSpecialAttackState();
    public AvatarSpecialAttackState SpecialAttackState => _specialAttackState;

    private AvatarJumpAttackState _jumpAttackState = new AvatarJumpAttackState();
    public AvatarJumpAttackState JumpAttackState => _jumpAttackState;

    private InputAction _moveAction;
    public InputAction MoveAction => _moveAction;

    private InputAction _jumpAction;
    public InputAction JumpAction => _jumpAction;

    private InputAction _attackAction;
    public InputAction AttackAction => _attackAction;

    private InputAction _specialAttackAction;
    public InputAction SpecialAttackAction => _specialAttackAction;

    [SerializeField] private Rigidbody _avatarRb;
    public Rigidbody AvatarRb => _avatarRb;

    [SerializeField] private PlayerInput _avatarInput;
    [SerializeField] private SphereCollider _avatarPunchCollider;
    public SphereCollider AvatarPunchCollider => _avatarPunchCollider;
    [SerializeField] private SphereCollider _avatarSpecialCollider;
    public SphereCollider AvatarSpecialCollider => _avatarSpecialCollider;

    [SerializeField] private Animator _avatarAnimator;
    public Animator AvatarAnimator => _avatarAnimator;

    [SerializeField] private SpecialPointBar _specialPointBar;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    private const string MOVEMENT_INPUT_ACTION = "Move";
    private const string JUMP_INPUT_ACTION = "Jump";
    private const string ATTACK_INPUT_ACTION = "Attack";
    private const string SPECIAL_ATTACK_INPUT_ACTION = "Special";
    private const string GROUND_TAG = "Ground";

    private void Start()
    {
        _currentState = _idleState;
        _moveAction = _avatarInput.actions.FindAction(MOVEMENT_INPUT_ACTION);
        _jumpAction = _avatarInput.actions.FindAction(JUMP_INPUT_ACTION);
        _attackAction = _avatarInput.actions.FindAction(ATTACK_INPUT_ACTION);
        _specialAttackAction = _avatarInput.actions.FindAction(SPECIAL_ATTACK_INPUT_ACTION);
        _specialPointBar.SetSpecialPointBar(_currentSpecialPoint, _maxSpecialPoint, _canUseSpecialAttack);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IncreaseSpecialPoint();
            other.GetComponent<DummyStateManager>()?.ReceiveDamage();
        }
    }

    public void IncreaseSpecialPoint()
    {
        if (_currentState == _specialAttackState)
        {
            return;
        }

        _currentSpecialPoint += 5;
        _specialPointBar.SetSpecialPointBar(_currentSpecialPoint, _maxSpecialPoint, _canUseSpecialAttack);
        if (_currentSpecialPoint >= _maxSpecialPoint)
        {
             _currentSpecialPoint = _maxSpecialPoint;
            _canUseSpecialAttack = true;
        }
    }

    public void ResetSpecialPoint()
    {
        _currentSpecialPoint = 0;
        _specialPointBar.SetSpecialPointBar(_currentSpecialPoint, _maxSpecialPoint, _canUseSpecialAttack);
    }

    public void ExecuteMusic(int musicIndex)
    {
        if (musicIndex >= _audioClips.Count)
            return;
        _audioSource.clip = _audioClips[musicIndex];
        _audioSource.Play();
    }
}
