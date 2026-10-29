using UnityEngine;

public class PlayerAttackState : IState, IFixedTickState
{
    private readonly Player _player;
    private readonly float _duration;
    private float _timeRemaining;
    private bool _stopOnGround;

    public PlayerAttackState(Player player, float duration)
    {
        _player = player;
        _duration = duration;
    }

    public void Enter()
    {
        _timeRemaining = _duration;
        _stopOnGround = _player.IsGrounded;
        _player.PlayAttackAnimation();
    }

    public void Tick()
    {
        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _player.ChangeToControl();
        }
    }

    public void FixedTick()
    {
        if (_stopOnGround)
        {
            _player.StopHorizontalMovement();
            return;
        }

        _player.Move();
    }

    public void Exit()
    {
        _player.FinishAttack();
    }
}
