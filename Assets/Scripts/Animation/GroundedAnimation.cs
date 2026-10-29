using System;
using UnityEngine;

public class GroundedAnimation : IAnimationParameter
{
    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");

    public void Apply(Animator animator, AnimationParameter parameter)
    {
        animator.SetBool(_isGroundedHash, parameter.IsGrounded);
    }
}