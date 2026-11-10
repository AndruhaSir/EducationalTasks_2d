using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthBarText : MonoBehaviour
{
    private TMP_Text _healthText;
    private string _label = "HP:";
    private int _maxHealth;

    private void Awake()
    {
        _healthText = GetComponent<TMP_Text>();
    }

    public void Initialize(int maxHealth, string label)
    {
        _maxHealth = maxHealth;
        _label = label;
    }

    public void SetValue(int newValue)
    {
        _healthText.text = $"{_label}{newValue}/{_maxHealth}";
    }
}