using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRotator))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    [Header("Ground Check")] 
    [SerializeField] private Transform _groundCheckPoint;

    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Movement")]
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private float _speed = 8f;

    [Header("Visual")]
    [SerializeField] private PlayerStatsUI _playerStatsUI;
    [SerializeField] private Animator _animator;

    private PlayerStats _playerStats;

    private Rigidbody2D _rigidbody;

    private CollisionHandler _collisionHandler;
    private PlayerInput _input;
    private Mover _mover;
    private GroundChecker _groundChecker;
    private Jumper _jumper;
    private AnimationSwitcher _animationSwitcher;
    private AnimationState _animationState;
    private SpriteRotator _spriteRotator;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _input = new PlayerInput(_jumpKey, _horizontalAxis);
        _groundChecker = new GroundChecker(_groundCheckPoint, _groundCheckRadius, _groundLayer);
        _jumper = new Jumper(_rigidbody, _jumpForce);
        _mover = new Mover(_rigidbody, _speed);
        _animationState = new AnimationState();
        _playerStats = new PlayerStats();
        _spriteRotator = GetComponent<SpriteRotator>();
        _playerStatsUI.UpdateCoins(_playerStats.Coins);

        _animationSwitcher = new AnimationSwitcher(_animator);
        _collisionHandler = GetComponent<CollisionHandler>();
        _animationSwitcher.AddBehaviour(new GroundedAnimation());
        _animationSwitcher.AddBehaviour(new SpeedAnimation());
    }

    private void OnEnable()
    {
        _collisionHandler.CoinEntered += CollectCoin;
    }

    private void OnDisable()
    {
        _collisionHandler.CoinEntered -= CollectCoin;
    }

    private void Update()
    {
        UpdateMovementState();

        Jump();

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)_groundCheckPoint.position, _groundCheckRadius);
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

    private void UpdateMovementState()
    {
        _input.Read();
        _isGrounded = _groundChecker.Check();
    }

    private void UpdateAnimations()
    {
        _spriteRotator.Flip(_input.HorizontalInput);

        _animationState.Speed = _input.HorizontalInput;
        _animationState.IsGrounded = _isGrounded;

        _animationSwitcher.Update(_animationState);
    }

    private void Move()
    {
        _mover.MoveGround(_input.HorizontalInput);
    }

    private void Jump()
    {
        if (_isGrounded && _input.JumpPressed)
        {
            _jumper.Jump();
        }
    }
}