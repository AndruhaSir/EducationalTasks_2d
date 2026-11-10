using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))] 
public class HealthBarSlider : MonoBehaviour
{
    private float _animationDuration = 0.75f;

    private Slider _slider;
    private Coroutine _animation;

    private void Awake()
    {
        _slider  = GetComponent<Slider>();
    }

    public void Initialize(float animationDuration)
    {
        _animationDuration = Mathf.Max(0.01f, animationDuration);
    }

    public void SetValue(float targetValue)
    {
        targetValue = Mathf.Clamp(targetValue, _slider.minValue, _slider.maxValue);
        float startValue = _slider.value;

        if (_animation != null)
        {
            StopCoroutine(_animation);
        }

        _animation = StartCoroutine(AnimateValue(startValue, targetValue));
    }

    public void SetValueImmediately(float targetValue)
    {
        targetValue = Mathf.Clamp(targetValue, _slider.minValue, _slider.maxValue);

        if (_animation != null)
        {
            StopCoroutine(_animation);
            _animation = null;
        }

        _slider.value = targetValue;
    }

    private IEnumerator AnimateValue(float startValue, float targetValue)
    {
        float distance = Mathf.Abs(targetValue - startValue);
        if (distance <= Mathf.Epsilon)
        {
            _slider.value = targetValue;
            _animation = null;
            yield break;
        }

        float elapsed = 0f;

        while (elapsed < _animationDuration)
        {
            elapsed += Time.deltaTime;
            _slider.value = Mathf.Lerp(startValue, targetValue, elapsed / _animationDuration);
            yield return null;
        }

        _slider.value = targetValue;
        _animation = null;
    }
}
