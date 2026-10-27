using System;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private TMP_Text _coinTextMeshPro;
    [SerializeField] private string _coinsText = "Coins:";

    private void OnValidate()
    {
        if (_playerStats == null)
        {
            Debug.LogWarning("Не все компоненты назначены", this);
            enabled = false;
        }
    }

    private void OnEnable()
    {
        _playerStats.CoinsChanged += UpdateCoinText;
        UpdateCoinText(_playerStats.Coins);
    }

    private void OnDisable()
    {
        _playerStats.CoinsChanged -= UpdateCoinText;
    }

    private void UpdateCoinText(int coins)
    {
        _coinTextMeshPro.text = $"{_coinsText} {coins}";
    }
}