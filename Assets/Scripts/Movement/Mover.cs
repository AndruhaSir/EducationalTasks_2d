using UnityEngine;

public class Mover
{
    private readonly Rigidbody2D _rigidbody;
    private readonly float _speed;

    public Mover(Rigidbody2D rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _speed = speed;
    }

    public void MoveGround(float horizontalDirection)
    {
        _rigidbody.velocity = new Vector2(horizontalDirection * _speed, _rigidbody.velocity.y);
    }

    public void MoveFlying(Vector2 direction)
    {
        _rigidbody.velocity = direction.normalized * _speed;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}
