using System;
using UnityEngine;

public class GroundedAnimation : IAnimationBehaviour
{
    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");

    public void Apply(Animator animator, AnimationState state)
    {
        animator.SetBool(_isGroundedHash, state.IsGrounded);
    }
}