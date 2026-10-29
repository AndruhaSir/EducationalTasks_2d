using System;

public class Health : IHealth, IDamageable
{
    public int Current { get; private set; }
    public int Max { get; }
    public bool IsDead => Current <= 0;
    public bool CanBeHealed { get; }

    public event Action HealthChanged;

    public Health(int max, bool canBeHealed = false)
    {
        Max = max;
        Current = max;
        CanBeHealed = canBeHealed;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || IsDead)
        {
            return;
        }

        Current = Math.Max(0, Current - damage);

        HealthChanged?.Invoke();
    }

    public void Heal(int amount)
    {
        if (IsDead || CanBeHealed == false)
        {
            return;
        }

        Current = Math.Min(Max, Current + amount);

        HealthChanged?.Invoke();
    }
}