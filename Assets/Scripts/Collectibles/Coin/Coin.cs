using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField, Min(1)] private int _value = 1;

    private bool _isCollected;

    public int Collect()
    {
        if (_isCollected)
        {
            return 0;
        }

        _isCollected = true;

        int value = _value;
        Destroy(gameObject);

        return value;
    }
}