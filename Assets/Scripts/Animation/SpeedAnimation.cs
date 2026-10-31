using UnityEngine;

public class SpeedAnimation : IAnimationWithParameter
{
    private readonly int _speedHash = Animator.StringToHash("Speed");

    public void Apply(Animator animator, AnimationParameter parameter)
    {
        animator.SetFloat(_speedHash, Mathf.Abs(parameter.Speed));
    }
}