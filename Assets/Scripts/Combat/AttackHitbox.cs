using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{
    [SerializeField, Min(1)] private int _damage = 10;
    private Collider2D _collider;
    private readonly HashSet<Hurtbox> _damagedTargets = new HashSet<Hurtbox>();

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _collider.enabled = false;
    }

    public void Open()
    {
        _damagedTargets.Clear();
        _collider.enabled = true;
    }

    public void Close()
    {
        _collider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox != null && _damagedTargets.Add(hurtbox))
        {
            hurtbox.TakeDamage(_damage);
        }
    }
}
