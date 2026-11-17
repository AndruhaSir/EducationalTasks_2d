using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _activationKeyText;
    [SerializeField] private Vampirism _vampirism;

    private void OnEnable()
    {
        _vampirism.ProgressChanged += SetProgress;
        _vampirism.ActivationKeyChanged += SetActivationKey;

        SetProgress(_vampirism.Progress, _vampirism.ProgressClockwise);
        SetActivationKey(_vampirism.ActivationKey);
    }

    private void OnDisable()
    {
        _vampirism.ProgressChanged -= SetProgress;
        _vampirism.ActivationKeyChanged -= SetActivationKey;
    }

    private void SetProgress(float progress, bool clockwise)
    {
        if (_fillImage == null)
        {
            return;
        }

        _fillImage.fillClockwise = clockwise;
        _fillImage.fillAmount = Mathf.Clamp01(progress);
    }

    private void SetActivationKey(KeyCode key)
    {
        if (_activationKeyText != null)
        {
            _activationKeyText.text = key.ToString();
        }
    }
}
