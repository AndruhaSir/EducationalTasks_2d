using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRotator))]
[RequireComponent(typeof(PatrolMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _useDirectMovement;

    [Header("Visual")]
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody;
    private SpriteRotator _spriteRotator;
    private PatrolMovement _patrolMovement;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;
    private AnimationState _animationState;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRotator = GetComponent<SpriteRotator>();
        _patrolMovement = GetComponent<PatrolMovement>();

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _mover = new Mover(_rigidbody, _speed);
        _animationState = new AnimationState();
        _animationSwitcher = new AnimationSwitcher(_animator);
        _animationSwitcher.AddBehaviour(new SpeedAnimation());
    }

    private void Update()
    {
        _animationState.Speed = _rigidbody.velocity.magnitude;
        _animationSwitcher.Update(_animationState);
    }

    private void FixedUpdate()
    {
        if (_patrolMovement.TryGetDirection(_rigidbody.position, out Vector2 direction) == false)
        {
            _mover.Stop();
            return;
        }

        if (_useDirectMovement)
        {
            _mover.MoveFlying(direction);
        }
        else
        {
            _mover.MoveGround(direction.x);
        }

        _spriteRotator.Flip(direction.x);
    }
}
