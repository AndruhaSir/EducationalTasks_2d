using System;
using UnityEngine;

public class DeathState : IState
{
    private readonly float _duration;
    private readonly Action _onEnter;
    private readonly Action _onComplete;
    private float _timeRemaining;
    private bool _isComplete;

    public DeathState(float duration, Action onEnter, Action onComplete = null)
    {
        _duration = duration;
        _onEnter = onEnter;
        _onComplete = onComplete;
    }

    public void Enter()
    {
        _timeRemaining = _duration;
        _isComplete = false;
        _onEnter?.Invoke();
    }

    public void Tick()
    {
        if (_isComplete)
        {
            return;
        }

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _isComplete = true;
            _onComplete?.Invoke();
        }
    }

    public void Exit() { }
}
