using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _delayedFill;
    [SerializeField] private string _label = "HP:";
    [SerializeField, Min(0.01f)] private float _delayedBarSpeed = 0.75f;

    private float _targetHealthRatio;
    private bool _isInitialized;

    private void Awake()
    {
        if (_slider != null)
        {
            _slider.minValue = 0f;
            _slider.maxValue = 1f;
        }
    }

    public void SetHealth(int currentHealth, int maximumHealth)
    {
        float healthRatio = Mathf.Clamp01((float)currentHealth / maximumHealth);

        if (!_isInitialized)
        {
            _targetHealthRatio = healthRatio;

            _slider.value = healthRatio;
            _delayedFill.fillAmount = healthRatio;

            _isInitialized = true;
        }
        else
        {
            if (healthRatio < _targetHealthRatio)
            {
                _slider.value = healthRatio;
            }
            else if (healthRatio > _targetHealthRatio)
            {
                _delayedFill.fillAmount = healthRatio;
            }

            _targetHealthRatio = healthRatio;
        }

        if (_healthText != null)
        {
            _healthText.text = $"{_label} {currentHealth}/{maximumHealth}";
        }

        Update();
    }

    private void Update()
    {
        if (!_isInitialized)
        {
            return;
        }

        float step = _delayedBarSpeed * Time.deltaTime;

        _slider.value = Mathf.MoveTowards(_slider.value, _targetHealthRatio, step);

        _delayedFill.fillAmount = Mathf.MoveTowards(_delayedFill.fillAmount, _targetHealthRatio, step);
    }
}