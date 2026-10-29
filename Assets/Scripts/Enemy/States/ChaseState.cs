using UnityEngine;

public class ChaseState : IState
{
    private readonly Enemy _enemy;

    public ChaseState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter() { }

    public void Tick()
    {
        if (_enemy.Target == null)
        {
            _enemy.ChangeToPatrol();
            return;
        }

        if (_enemy.TargetInAttackRange)
        {
            _enemy.Stop();

            if (_enemy.CanAttack)
            {
                _enemy.ChangeToAttack();
            }

            return;
        }

        Vector2 offset = (Vector2)_enemy.Target.position - _enemy.Rigidbody.position;
        _enemy.Move(offset.normalized);
    }

    public void Exit() { }
}
