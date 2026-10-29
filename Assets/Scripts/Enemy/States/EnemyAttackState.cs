using UnityEngine;

public class EnemyAttackState : IState
{
    private readonly Enemy _enemy;
    private readonly float _duration;
    private float _timeRemaining;

    public EnemyAttackState(Enemy enemy, float duration)
    {
        _enemy = enemy;
        _duration = duration;
    }

    public void Enter()
    {
        _timeRemaining = _duration;
        _enemy.Stop();
        _enemy.BeginAttack();
    }

    public void Tick()
    {
        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _enemy.FinishAttack();
        }
    }

    public void Exit()
    {
        _enemy.CloseAttackHitbox();
    }
}
