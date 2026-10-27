using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _coins;

    public int Coins => _coins;

    public event Action<int> CoinsChanged;

    public void AddCoins(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _coins += amount;
        CoinsChanged?.Invoke(Coins);
    }
}