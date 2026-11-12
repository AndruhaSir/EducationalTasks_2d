using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _activationKeyText;

    public void SetProgress(float progress, bool clockwise = true)
    {
        if (_fillImage != null)
        {
            _fillImage.fillClockwise = clockwise;
            _fillImage.fillAmount = Mathf.Clamp01(progress);
        }
    }

    public void SetActivationKey(KeyCode key)
    {
        if (_activationKeyText != null)
        {
            _activationKeyText.text = key.ToString();
        }
    }
}