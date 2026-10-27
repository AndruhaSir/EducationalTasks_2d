using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMovement : MonoBehaviour
{
    [SerializeField] private WaypointPath _path;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _waitTimeAtPoint = 1f;
    [SerializeField] private float _arrivalThreshold = 0.2f;
    [SerializeField] private bool _useDirectMovement = false;

    private Rigidbody2D _rigidbody;
    private SpriteFlipper _spriteFlipper;
    private int _currentPointIndex = 0;
    private bool _isWaiting = false;

    private Coroutine _waitCoroutine;

    private void Awake()
    {
        if (_path == null || _path.Count == 0)
        {
            Debug.LogError("Не все компоненты назначены", this);
            enabled = false;
            return;
        }
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteFlipper = GetComponent<SpriteFlipper>();
    }

    private void Start()
    {
        _currentPointIndex = _path.GetClosestPointIndex(transform.position);
    }

    private void FixedUpdate()
    {
        if (_path.Count == 0 || _isWaiting) return;

        Move();
    }

    private bool HasReachedTarget(Vector3 targetPosition)
    {
        return (targetPosition - transform.position).sqrMagnitude < _arrivalThreshold * _arrivalThreshold;
    }

    private void Move()
    {
        Vector3 targetPosition = _path.GetPointPosition(_currentPointIndex);

        if (HasReachedTarget(targetPosition))
        {
            StartWaiting();
            return;
        }

        Vector3 direction = (targetPosition - transform.position).normalized;

        if (_useDirectMovement)
        {
            _rigidbody.velocity = direction * _speed;
        }
        else
        {
            _rigidbody.velocity = new Vector2(direction.x * _speed, _rigidbody.velocity.y);
        }

        _spriteFlipper.Flip(direction.x);
    }

    private void StartWaiting()
    {
        _isWaiting = true;
        _rigidbody.velocity = Vector2.zero;

        if (_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);

        _waitCoroutine = StartCoroutine(WaitAtPointRoutine());
    }

    private IEnumerator WaitAtPointRoutine()
    {
        yield return new WaitForSeconds(_waitTimeAtPoint);

        _currentPointIndex++;
        _isWaiting = false;
        _waitCoroutine = null;
    }
}