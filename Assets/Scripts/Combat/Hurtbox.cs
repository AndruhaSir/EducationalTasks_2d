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

    public void TakeDamage(int damage)
    {
        _damageable?.TakeDamage(damage);
    }
}
