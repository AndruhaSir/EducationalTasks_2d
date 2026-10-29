using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    private Enemy _enemy;

    private readonly HashSet<Collider2D> _playerColliders = new HashSet<Collider2D>();

    private void Awake()
    {
        if (_enemy == null)
        {
            _enemy = GetComponentInParent<Enemy>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<Player>() == null)
        {
            return;
        }

        _playerColliders.Add(other);
        _enemy.SetTargetInAttackRange(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<Player>() == null)
        {
            return;
        }

        _playerColliders.Remove(other);
        _enemy.SetTargetInAttackRange(_playerColliders.Count > 0);
    }

    private void OnDisable()
    {
        _playerColliders.Clear();

        if (_enemy != null)
        {
            _enemy.SetTargetInAttackRange(false);
        }
    }
}
