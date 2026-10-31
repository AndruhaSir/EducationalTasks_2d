using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Coin> CoinEntered;
    public event Action<Medkit> MedkitEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollectablesItems(other);
    }

    private void CheckCollectablesItems(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            CoinEntered?.Invoke(coin);
            return;
        }

        if (other.TryGetComponent(out Medkit medkit))
        {
            MedkitEntered?.Invoke(medkit);
        }
    }
}