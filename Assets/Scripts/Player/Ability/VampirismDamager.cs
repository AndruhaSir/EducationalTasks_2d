using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VampirismDamager : MonoBehaviour
{
    private int _damagePerTick = 5;

    private readonly HashSet<Collider2D> _targetsInRange = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hurtbox>(out _))
        {
            _targetsInRange.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _targetsInRange.Remove(other);
    }

    private void OnDisable()
    {
        _targetsInRange.Clear();
    }

    public void Initialize(int damagePerTick)
    {
        _damagePerTick  = damagePerTick;
    }

    public void Tick(IHealth playerHealth, float deltaTime)
    {
        Hurtbox target = FindNearestEnemy();
        if (target == null)
        {
            return;
        }

        int dealtDamage = target.TakeDamage(_damagePerTick);
        playerHealth.Heal(dealtDamage);
    }

    private Hurtbox FindNearestEnemy()
    {
        _targetsInRange.RemoveWhere(collider => collider == null);

        Hurtbox nearestEnemy = null;
        float nearestDistance = float.MaxValue;
        Vector2 center = transform.position;

        foreach (Collider2D collider in _targetsInRange)
        {
            if (collider.TryGetComponent<Hurtbox>(out Hurtbox enemyHurtbox) == false)
            {
                continue;
            }

            float distance = ((Vector2)enemyHurtbox.transform.position - center).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemyHurtbox;
            }
        }

        return nearestEnemy;
    }
}