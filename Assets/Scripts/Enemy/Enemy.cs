using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRotator))]
[RequireComponent(typeof(PatrolMovement))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Movement")] [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _useDirectMovement;
    [SerializeField, Min(1)] private int _startHitpoints = 10;
    [SerializeField, Min(0.01f)] private float _attackCooldown = 1.5f;
    [SerializeField, Min(0.01f)] private float _attackDuration = 0.67f;

    [Header("Visual")] [SerializeField] private Animator _animator;
    [SerializeField, Min(0.01f)] private float _deathAnimationDuration = 1.03f;

    [Header("Combat")] [SerializeField] private AttackHitbox _attackHitbox;

    private Rigidbody2D _rigidbody;
    private SpriteRotator _spriteRotator;
    private PatrolMovement _patrolMovement;
    private Mover _mover;
    private AnimationSwitcher _animationSwitcher;
    private Health _health;

    private StateMachine<IState> _stateMachine;
    private EnemyPatrolState _enemyPatrolState;
    private EnemyChaseState _enemyChaseState;
    private EnemyAttackState _attackState;
    private DeathState _deathState;

    private AnimationParameter _animationParameter;
    private bool _targetInAttackRange;
    private float _nextAttackTime;

    public Rigidbody2D Rigidbody => _rigidbody;
    public PatrolMovement PatrolMovement => _patrolMovement;
    public Transform Target { get; private set; }
    public bool TargetInAttackRange => _targetInAttackRange;
    public bool CanAttack => Time.time >= _nextAttackTime;
    public bool IsDead => _health.IsDead;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRotator = GetComponent<SpriteRotator>();
        _patrolMovement = GetComponent<PatrolMovement>();

        _mover = new Mover(_rigidbody, _speed);
        _health = new Health(_startHitpoints);

        _stateMachine = new StateMachine<IState>();
        _enemyPatrolState = new EnemyPatrolState(this);
        _enemyChaseState = new EnemyChaseState(this);
        _attackState = new EnemyAttackState(this, _attackDuration);
        _animationParameter = new AnimationParameter();
        _deathState = new DeathState(_deathAnimationDuration, EnterDeath, () => Destroy(this.gameObject));

        _animationSwitcher = new AnimationSwitcher(_animator);
        _animationSwitcher.AddParameter(new SpeedAnimation());
    }

    private void Start()
    {
        _stateMachine.ChangeState(_enemyPatrolState);
    }

    private void Update()
    {
        _animationParameter.Speed = _rigidbody.velocity.magnitude;
        _animationSwitcher.Update(_animationParameter);
    }

    private void FixedUpdate()
    {
        _stateMachine.Tick();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void ClearTarget(Transform target)
    {
        if (Target == target)
        {
            Target = null;
            _targetInAttackRange = false;
        }
    }

    public void PlayDamageAnimation()
    {
        _animationSwitcher.PlayTakeHit();
    }

    public void SetTargetInAttackRange(bool isInRange)
    {
        _targetInAttackRange = isInRange;
    }

    public void ChangeToPatrol()
    {
        _stateMachine.ChangeState(_enemyPatrolState);
    }

    public void ChangeToChase()
    {
        _stateMachine.ChangeState(_enemyChaseState);
    }

    public void ChangeToAttack()
    {
        if (CanAttack && _targetInAttackRange && !_health.IsDead)
        {
            _stateMachine.ChangeState(_attackState);
        }
    }

    public void BeginAttack()
    {
        _nextAttackTime = Time.time + _attackCooldown;
        _animationSwitcher.PlayAttack();
    }

    public void FinishAttack()
    {
        ChangeToChase();
    }

    public void OpenAttackHitbox()
    {
        _attackHitbox?.Open();
    }

    public void CloseAttackHitbox()
    {
        _attackHitbox?.Close();
    }

    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            Stop();
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

    public void Stop()
    {
        _mover.Stop();
    }

    public void TakeDamage(int damage)
    {
        if (_health.IsDead)
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
            PlayDamageAnimation();
        }
    }

    private void EnterDeath()
    {
        _mover.Stop();
        _targetInAttackRange = false;
        CloseAttackHitbox();

        foreach (Collider2D enemyCollider in GetComponentsInChildren<Collider2D>())
        {
            enemyCollider.enabled = false;
        }

        _animationSwitcher.PlayDeath();
    }
}