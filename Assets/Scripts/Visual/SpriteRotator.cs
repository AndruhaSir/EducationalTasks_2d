using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _startDirectionRight = true;

    private Quaternion _rotationRight = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _rotationLeft = Quaternion.Euler(0f, 180f, 0f);

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if (!_startDirectionRight)
        {
            (_rotationRight, _rotationLeft) = (_rotationLeft, _rotationRight);
        }
    }

    public void Flip(float horizontalDirection)
    {
        if (Mathf.Approximately(horizontalDirection, 0f)) return;

        _animator.transform.localRotation = horizontalDirection > 0f
            ? _rotationRight
            : _rotationLeft;
    }
}
