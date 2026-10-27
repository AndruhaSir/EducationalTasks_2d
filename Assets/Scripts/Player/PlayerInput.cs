using UnityEngine;

public class PlayerInput
{
    private readonly KeyCode _jumpKey;
    private readonly string _horizontalAxis;

    public float HorizontalInput { get; private set; }
    public bool JumpPressed { get; private set; }

    public PlayerInput(KeyCode jumpKey, string horizontalAxis)
    {
        _jumpKey = jumpKey;
        _horizontalAxis = horizontalAxis;
    }

    public void Read()
    {
        HorizontalInput = Input.GetAxisRaw(_horizontalAxis);
        JumpPressed = Input.GetKeyDown(_jumpKey);
    }
}