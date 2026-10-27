public class PlayerStats
{
    public int Coins { get; private set; }

    public void AddCoins(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        Coins += amount;
    }
}