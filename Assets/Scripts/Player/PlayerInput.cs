using UnityEngine;

public class PlayerInput
{
    private readonly KeyCode _jumpKey;
    private readonly KeyCode _attackKey;
    private readonly string _horizontalAxis;

    public float HorizontalInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    public PlayerInput(KeyCode jumpKey, string horizontalAxis, KeyCode attackKey)
    {
        _jumpKey = jumpKey;
        _horizontalAxis = horizontalAxis;
        _attackKey = attackKey;
    }

    public void Read()
    {
        HorizontalInput = Input.GetAxisRaw(_horizontalAxis);
        JumpPressed = Input.GetKeyDown(_jumpKey);
        AttackPressed = Input.GetKeyDown(_attackKey);
    }
}
