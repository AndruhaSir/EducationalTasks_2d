using System.Collections;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] private WaypointPath _path;
    [SerializeField] private float _waitTimeAtPoint = 1f;
    [SerializeField] private float _arrivalThreshold = 0.2f;

    private int _currentPointIndex;
    private bool _isWaiting;

    private float ArrivalThresholdSqr => _arrivalThreshold * _arrivalThreshold;

    private void Awake()
    {
        if (_path == null)
        {
            Debug.LogError($"Не назначен компонент {nameof(_path)}", this);
            enabled = false;
            return;
        }

        _currentPointIndex = _path.GetClosestPointIndex(transform.position);
    }

    public bool TryGetDirection(Vector2 position, out Vector2 direction)
    {
        direction = Vector2.zero;
        
        if (enabled == false || _isWaiting)
        {
            return false;
        }

        Vector2 targetPosition = _path.GetPointPosition(_currentPointIndex);
        Vector2 offset = targetPosition - position;

        if (offset.sqrMagnitude < ArrivalThresholdSqr)
        {
            StartCoroutine(WaitAtPoint());
            return false;
        }

        direction = offset.normalized;
        return true;
    }

    private IEnumerator WaitAtPoint()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_waitTimeAtPoint);

        _currentPointIndex++;
        _isWaiting = false;
    }
}
