using UnityEngine;

public class SpeedAnimation : IAnimationBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("Speed");

    public void Apply(Animator animator, AnimationState state)
    {
        animator.SetFloat(_speedHash, Mathf.Abs(state.Speed));
    }
}