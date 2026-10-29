using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private readonly HashSet<Collider2D> _playerColliders = new HashSet<Collider2D>();
    private Player _targetPlayer;

    private void Awake()
    {
        if (_enemy == null)
        {
            _enemy = GetComponentInParent<Enemy>();
        }

        if (_enemy == null)
        {
            Debug.LogError($"Не назначен компонент {nameof(_enemy)}", this);
            enabled = false;
        }
    }

    private void OnDisable()
    {
        if (_targetPlayer != null && _enemy != null)
        {
            _enemy.ClearTarget(_targetPlayer.transform);
        }

        _playerColliders.Clear();
        _targetPlayer = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player == null || (_targetPlayer != null && _targetPlayer != player))
        {
            return;
        }

        _targetPlayer = player;
        _playerColliders.Add(other);
        _enemy.SetTarget(player.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player == null || player != _targetPlayer)
        {
            return;
        }

        _playerColliders.Remove(other);

        if (_playerColliders.Count == 0)
        {
            _enemy.ClearTarget(player.transform);
            _targetPlayer = null;
        }
    }
}
