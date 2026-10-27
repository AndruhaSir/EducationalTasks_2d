using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _smoothTime = 0.15f;
    [SerializeField] private Vector2 _offset = Vector2.zero;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
          if (_target == null) return;

        Vector3 desired = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref _velocity, _smoothTime);
    }
}