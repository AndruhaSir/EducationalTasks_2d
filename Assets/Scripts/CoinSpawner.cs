using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField, Min(0)] private int _coinCount = 5;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        if (_coinPrefab == null || _spawnPoints == null || _spawnPoints.Length == 0)
        {
            return;
        }

        List<Transform> availablePoints = new List<Transform>();

        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint != null)
            {
                availablePoints.Add(spawnPoint);
                Debug.Log($"Добавлена точка: {spawnPoint.name}, позиция: {spawnPoint.position}");
            }
        }

        int spawnCount = Mathf.Min(_coinCount, availablePoints.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[randomIndex];

            Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);

            availablePoints.RemoveAt(randomIndex);
        }
    }
}