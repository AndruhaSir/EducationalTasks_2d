using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRotator))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour, IDamageable
{
    [Header("Ground Check")] [SerializeField]
    private Transform _groundCheckPoint;

    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Jump")] [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private bool _showGroundCheckGizmos = true;

    [Header("Movement")] [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private float _speed = 8f;

    [Header("Combat")] [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
    [SerializeField, Min(0.01f)] private float _attackStateDuration = 0.52f;
    [SerializeField, Min(0.01f)] private float _invulnerabilityDuration = 1f;
    [SerializeField, Min(0.01f)] private float _deathAnimationDuration = 1.03f;
    [SerializeField] private AttackHitbox2D _attackHitbox;

    [Header("Visual")] [SerializeField] private PlayerStatsUI _playerStatsUI;
    [SerializeField] private Animator _animator;

    [Header("Stats")] [SerializeField] private int _startHitpoints = 100;

    private PlayerStats _playerStats;

    private Rigidbody2D _rigidbody;

    private CollisionHandler _collisionHandler;
    private PlayerInput _input;
    private Mover _mover;
    private GroundChecker _groundChecker;
    private Jumper _jumper;
    private AnimationSwitcher _animationSwitcher;
    private AnimationParameter _animationParameter;
    private SpriteRotator _spriteRotator;
    private Health _health;
    private StateMachine<IState> _stateMachine;
    private PlayerControlState _controlState;
    private PlayerAttackState _attackState;
    private DeathState _deathState;
    private bool _isGrounded;
    private float _invulnerabilityTimeRemaining;
    private bool _pendingDamageAnimation;

    public bool AttackPressed => _input.AttackPressed;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _input = new PlayerInput(_jumpKey, _horizontalAxis, _attackKey);
        _groundChecker = new GroundChecker(_groundCheckPoint, _groundCheckRadius, _groundLayer);
        _jumper = new Jumper(_rigidbody, _jumpForce);
        _mover = new Mover(_rigidbody, _speed);
        _animationParameter = new AnimationParameter();
        _playerStats = new PlayerStats();
        _health = new Health(_startHitpoints, true);

        _spriteRotator = GetComponent<SpriteRotator>();

        _playerStatsUI.UpdateCoins(_playerStats.Coins);
        _playerStatsUI.UpdateHitpoints(_health.Current);

        _collisionHandler = GetComponent<CollisionHandler>();
        _animationSwitcher = new AnimationSwitcher(_animator);
        _animationSwitcher.AddParameter(new GroundedAnimation());
        _animationSwitcher.AddParameter(new SpeedAnimation());

        _stateMachine = new StateMachine<IState>();
        _controlState = new PlayerControlState(this);
        _attackState = new PlayerAttackState(this, _attackStateDuration);
        _deathState = new DeathState(_deathAnimationDuration, EnterDeath);
    }

    private void Start()
    {
        ChangeToControl();
    }

    private void OnEnable()
    {
        _collisionHandler.CoinEntered += CollectCoin;
        _collisionHandler.MedkitEntered += CollectMedkit;
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _collisionHandler.CoinEntered -= CollectCoin;
        _collisionHandler.MedkitEntered -= CollectMedkit;
        _health.HealthChanged -= OnHealthChanged;
    }

    private void Update()
    {
        if (_invulnerabilityTimeRemaining > 0f)
        {
            _invulnerabilityTimeRemaining = Mathf.Max(
                0f,
                _invulnerabilityTimeRemaining - Time.deltaTime);
        }

        _input.Read();
        _isGrounded = _groundChecker.Check();
        _animationParameter.Speed = _input.HorizontalInput;
        _animationParameter.IsGrounded = _isGrounded;
        _stateMachine.Tick();
        _animationSwitcher.Update(_animationParameter);
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedTick();
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckPoint != null && _showGroundCheckGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)_groundCheckPoint.position, _groundCheckRadius);
        }
    }

    private void CollectMedkit(Medkit medkit)
    {
        medkit.Collect(_health);
    }

    private void OnHealthChanged()
    {
        _playerStatsUI.UpdateHitpoints(_health.Current);
    }

    private void CollectCoin(Coin coin)
    {
        int value = coin.Collect();

        if (value <= 0)
        {
            return;
        }

        _playerStats.AddCoins(value);
        _playerStatsUI.UpdateCoins(_playerStats.Coins);
    }

    public void TakeDamage(int damage)
    {
        if (_health.IsDead || _invulnerabilityTimeRemaining > 0f)
        {
            return;
        }

        _health.TakeDamage(damage);

        if (_health.IsDead)
        {
            _stateMachine.ChangeState(_deathState);
        }
        else
        {
            _invulnerabilityTimeRemaining = _invulnerabilityDuration;

            if (ReferenceEquals(_stateMachine.CurrentState, _attackState))
            {
                _pendingDamageAnimation = true;
            }
            else
            {
                PlayDamageAnimation();
            }
        }
    }

    public void ChangeToControl()
    {
        _stateMachine.ChangeState(_controlState);
    }

    public void ChangeToAttack()
    {
        _stateMachine.ChangeState(_attackState);
    }

    public void UpdateMovementInput()
    {
        _spriteRotator.Flip(_input.HorizontalInput);

        if (_isGrounded && _input.JumpPressed)
        {
            _jumper.Jump();
        }
    }

    public void Move()
    {
        _mover.MoveGround(_input.HorizontalInput);
    }

    public void StopHorizontalMovement()
    {
        _mover.MoveGround(0f);
    }

    public void PlayAttackAnimation()
    {
        _animationSwitcher.PlayAttack();
    }

    public void PlayDamageAnimation()
    {
        _animationSwitcher.PlayTakeHit();
    }

    public void OpenAttackHitbox()
    {
        _attackHitbox.Open();
    }

    public void CloseAttackHitbox()
    {
        _attackHitbox.Close();
    }

    public void FinishAttack()
    {
        CloseAttackHitbox();

        if (_pendingDamageAnimation)
        {
            _pendingDamageAnimation = false;

            if (_health.IsDead == false)
            {
                PlayDamageAnimation();
            }
        }
    }

    private void EnterDeath()
    {
        _mover.Stop();
        _animationSwitcher.PlayDeath();
    }
}
