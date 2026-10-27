using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField, Min(1)] private int _value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerStats playerStats))
        {
            return;
        }

        playerStats.AddCoins(_value);
        Destroy(gameObject);
    }
}