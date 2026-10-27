using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _speed = 5f;
    [SerializeField, Min(0f)] private float _jumpForce = 12f;
    [SerializeField, Min(0.01f)] private float _groundCheckRadius = 0.15f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded => _isGrounded;

    private bool _isGrounded;
    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (_playerInput != null)
        {
            _playerInput.JumpPressed += Jump;
        }
    }

    private void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.JumpPressed -= Jump;
        }
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_playerInput.HorizontalInput * _speed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}