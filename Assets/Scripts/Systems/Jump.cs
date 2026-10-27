using UnityEngine;

public class Jumper
{
    private readonly Rigidbody2D _rigidbody;
    private readonly float _jumpForce;

    public Jumper(Rigidbody2D rigidbody, float jumpForce)
    {
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}