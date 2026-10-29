using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField, Min(1)] private int _healAmount = 25;

    public void Collect(IHealth health)
    {
        health.Heal(_healAmount);
        Destroy(gameObject);
    }
}