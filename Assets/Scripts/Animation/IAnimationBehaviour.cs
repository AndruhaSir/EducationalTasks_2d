using UnityEngine;

public interface IAnimationBehaviour
{
    void Apply(Animator animator, AnimationState state);
}