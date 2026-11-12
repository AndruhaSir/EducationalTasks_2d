using UnityEngine;

public class EnemyBodyDamage : MonoBehaviour
{
    [SerializeField, Min(1)] private int _damage = 10;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_enemy != null && _enemy.IsDead)
        {
            return;
        }

        if (other.TryGetComponent<Hurtbox>(out Hurtbox otherHurtbox))
        {
            otherHurtbox.TakeDamage(_damage);
        }
    }
}
