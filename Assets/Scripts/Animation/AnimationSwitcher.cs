using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitcher
{
    private readonly Animator _animator;
    private readonly List<IAnimationParameter> _parameters = new List<IAnimationParameter>();
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int _takeHitHash = Animator.StringToHash("TakeHit");
    private readonly int _deathHash = Animator.StringToHash("Death");

    private AnimationParameter _previousParameter;
    private bool _hasPreviousState;

    public AnimationSwitcher(Animator animator)
    {
        _animator = animator;
    }

    public void AddParameter(IAnimationParameter parameter)
    {
        _parameters.Add(parameter);
    }
    
    public void Update(AnimationParameter parameter)
    {
        if (_hasPreviousState &&
            Mathf.Approximately(_previousParameter.Speed, parameter.Speed) &&
            _previousParameter.IsGrounded == parameter.IsGrounded)
        {
            return;
        }

        foreach (IAnimationParameter behaviour in _parameters)
        {
            behaviour.Apply(_animator, parameter);
        }

        _previousParameter = new AnimationParameter
        {
            Speed = parameter.Speed,
            IsGrounded = parameter.IsGrounded
        };

        _hasPreviousState = true;
    }

    public void PlayAttack() => _animator.SetTrigger(_attackHash);

    public void PlayTakeHit() => _animator.SetTrigger(_takeHitHash);

    public void PlayDeath()
    {
        _animator.ResetTrigger(_attackHash);
        _animator.ResetTrigger(_takeHitHash);
        _animator.SetTrigger(_deathHash);
    }
}
