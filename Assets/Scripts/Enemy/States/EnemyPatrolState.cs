using UnityEngine;

public class EnemyPatrolState : IState
{
    private readonly Enemy _enemy;

    public EnemyPatrolState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter() { }

    public void Tick()
    {
        if (_enemy.Target != null)
        {
            _enemy.ChangeToChase();
            return;
        }

        if (_enemy.PatrolMovement.TryGetDirection(_enemy.Rigidbody.position, out Vector2 direction))
        {
            _enemy.Move(direction);
        }
        else
        {
            _enemy.Stop();
        }
    }

    public void Exit() { }
}
