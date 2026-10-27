using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private string _coinsText = "Coins:";

    public void UpdateCoins(int coins)
    {
        _coinText.text = $"{_coinsText} {coins}";
    }
}