using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jump = KeyCode.Space;

    public float HorizontalInput { get; private set; }
    public event Action JumpPressed;

    private string _horizontal = "Horizontal";

    private void Update()
    {
        ReadMovementInput();
        ReadJumpInput();
    }

    private void ReadMovementInput()
    {
        HorizontalInput = Input.GetAxisRaw(_horizontal);
    }

    private void ReadJumpInput()
    {
        if (Input.GetKeyDown(_jump))
        {
            JumpPressed?.Invoke();
        }
    }
}