public interface IHealth
{
    int Current { get; }
    int Max { get; }
    bool IsDead { get; }
    bool CanBeHealed { get; }

    void Heal(int amount);
}