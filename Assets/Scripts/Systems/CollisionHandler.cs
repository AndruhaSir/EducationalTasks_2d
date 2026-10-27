using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Coin> CoinEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Coin coin))
        {
            return;
        }

        CoinEntered?.Invoke(coin);
    }
}