using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitcher
{
    private readonly Animator _animator;
    private readonly List<IAnimationBehaviour> _behaviours = new List<IAnimationBehaviour>();

    private AnimationState _previousState;
    private bool _hasPreviousState;

    public AnimationSwitcher(Animator animator)
    {
        _animator = animator;
    }

    public void AddBehaviour(IAnimationBehaviour behaviour)
    {
        _behaviours.Add(behaviour);
    }

    public void ClearBehaviours()
    {
        _behaviours.Clear();
        _hasPreviousState = false;
    }

    public void Update(AnimationState state)
    {
        if (_hasPreviousState && Mathf.Approximately(_previousState.Speed, state.Speed) &&
            _previousState.IsGrounded == state.IsGrounded)
        {
            return;
        }

        foreach (IAnimationBehaviour behaviour in _behaviours)
        {
            behaviour.Apply(_animator, state);
        }

        _previousState = new AnimationState
        {
            Speed = state.Speed,
            IsGrounded = state.IsGrounded
        };

        _hasPreviousState = true;
    }
}