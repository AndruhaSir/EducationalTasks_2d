using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthBarSlider _mainSlider;
    [SerializeField] private HealthBarSlider _subSlider;
    [SerializeField] private HealthBarText _textSlider;

    [SerializeField] private string _labelText = "HP:";

    [SerializeField, Min(0.01f)] private float _mainSliderDuration = 2f;
    [SerializeField, Min(0.01f)] private float _subSliderDuration = 0.75f;

    private int _maxHealth;
    private float _lastHealthRatio;
    private bool _hasPreviousHealthValue;

    private void Awake()
    {
        _mainSlider.Initialize(_mainSliderDuration);
        _subSlider.Initialize(_subSliderDuration);
    }

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _textSlider?.Initialize(_maxHealth, _labelText);
    }

    public void UpdateVisual(int newValue)
    {
        float healthRatio = Mathf.Clamp01((float)newValue / _maxHealth);
        _mainSlider.SetValue(healthRatio);

        if (_hasPreviousHealthValue && healthRatio > _lastHealthRatio)
        {
            _subSlider.SetValueImmediately(healthRatio);
        }
        else
        {
            _subSlider.SetValue(healthRatio);
        }

        _textSlider?.SetValue(newValue);
        _lastHealthRatio = healthRatio;
        _hasPreviousHealthValue = true;
    }
}