using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsTextMeshPro;
    [SerializeField] private TMP_Text _healthTextMeshPro;
    [SerializeField] private string _coinsText = "Coins:";
    [SerializeField] private string _healthText = "HP:";

    public void UpdateCoins(int coins)
    {
        _coinsTextMeshPro.text = $"{_coinsText} {coins}";
    }
    
    public void UpdateHitpoints(int hitpoints)
    {
        _healthTextMeshPro.text = $"{_healthText} {hitpoints}";
    }
}