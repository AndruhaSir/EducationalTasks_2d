using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private const string SpeedParameter = "Speed";
    [SerializeField] private const string IsGroundedParameter = "IsGrounded";

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteFlipper _spriteFlipper;

    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateDirection();
        UpdateMovementAnimation();
        UpdateJumpAnimation();
    }


    private void UpdateDirection()
    {
        _spriteFlipper.Flip(_playerInput.HorizontalInput);
    }

    private void UpdateMovementAnimation()
    {
        _animator.SetFloat(SpeedParameter, Mathf.Abs(_playerInput.HorizontalInput));
    }
    private void UpdateJumpAnimation()
    {
        if (_playerMovement.IsGrounded == true)
        {
            _animator.SetBool(IsGroundedParameter, true);
        }
        else
        {
            _animator.SetBool(IsGroundedParameter, false);
        }
    }
}