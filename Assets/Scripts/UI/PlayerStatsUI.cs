using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsTextMeshPro;
    [SerializeField] private string _coinsText = "Coins:";

    public void UpdateCoins(int coins)
    {
        _coinsTextMeshPro.text = $"{_coinsText} {coins}";
    }
}
