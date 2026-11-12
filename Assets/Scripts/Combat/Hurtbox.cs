using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private IDamageable _damageable;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();

        if (_damageable == null)
        {
            Debug.LogError($"{nameof(Hurtbox)} не все компоненты подключены", this);
            enabled = false;
        }
    }

    public int TakeDamage(int damage)
    {
        return _damageable.TakeDamage(damage);
    }
}
