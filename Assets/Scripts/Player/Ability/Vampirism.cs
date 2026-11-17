using System.Collections;
using System;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
   private float _abilityDuration;
   private float _cooldownDuration;
   private float _damageCooldown;

    [SerializeField] private VampirismDamager _damager;
    private Coroutine _abilityCoroutine;
    private KeyCode _activationKey;

    public event Action<float, bool> ProgressChanged;
    public event Action<KeyCode> ActivationKeyChanged;

    public float Progress { get; private set; }
    public bool ProgressClockwise { get; private set; } = true;
    public KeyCode ActivationKey => _activationKey;

    private void Awake()
    {
        SetProgress(0f);
    }

    public void Initialize(KeyCode key, float abilityDuration, float cooldownDuration, float damageCooldown, int damagePerTick)
    {
        _activationKey = key;
        ActivationKeyChanged?.Invoke(key);
        _abilityDuration = abilityDuration;
        _cooldownDuration = cooldownDuration;
        _damageCooldown = damageCooldown;

        _damager.Initialize(damagePerTick);
    }

    public void UseAbility(IHealth playerHealth)
    {
        if (_abilityCoroutine == null)
        {
            _damager.gameObject.SetActive(true);
            _abilityCoroutine = StartCoroutine(AbilityWork(playerHealth));
        }
    }

    private IEnumerator AbilityWork(IHealth playerHealth)
    {
        float elapsed = 0f;
        float damageTimer = 0;

        while (elapsed < _abilityDuration)
        {
            float deltaTime = Mathf.Min(Time.deltaTime, _abilityDuration - elapsed);
            
            
            if (elapsed >= damageTimer + _damageCooldown)
            {
                _damager.Tick(playerHealth, deltaTime);
                damageTimer = elapsed;
            }

            elapsed += deltaTime;
            SetProgress(elapsed / _abilityDuration);

            if (elapsed < _abilityDuration)
            {
                yield return null;
            }
        }

        _damager.gameObject.SetActive(false);
        elapsed = 0f;
        SetProgress(1f);

        while (elapsed < _cooldownDuration)
        {
            yield return null;

            float deltaTime = Mathf.Min(Time.deltaTime, _cooldownDuration - elapsed);
            elapsed += deltaTime;
            SetProgress(1f - elapsed / _cooldownDuration, false);
        }

        SetProgress(0f);
        _abilityCoroutine = null;
    }

    private void SetProgress(float progress, bool clockwise = true)
    {
        Progress = progress;
        ProgressClockwise = clockwise;
        ProgressChanged?.Invoke(progress, clockwise);
    }
}
