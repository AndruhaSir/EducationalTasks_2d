using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
   private float _abilityDuration;
   private float _cooldownDuration;
   private float _damageCooldown;

    [SerializeField] private VampirismDamager _damager;
    [SerializeField] private VampirismView _view;

    private Coroutine _abilityCoroutine;

    private void Awake()
    {
        if (_damager == null)
        {
            _damager = GetComponent<VampirismDamager>();
        }

        _view.SetProgress(0f);
    }

    public void Initialize(KeyCode key, float abilityDuration, float cooldownDuration, float damageCooldown, int damagePerTick)
    {
        _view.SetActivationKey(key);
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
            _view.SetProgress(elapsed / _abilityDuration);

            if (elapsed < _abilityDuration)
            {
                yield return null;
            }
        }

        _damager.gameObject.SetActive(false);
        elapsed = 0f;
        _view.SetProgress(1f);

        while (elapsed < _cooldownDuration)
        {
            yield return null;

            float deltaTime = Mathf.Min(Time.deltaTime, _cooldownDuration - elapsed);
            elapsed += deltaTime;
            _view.SetProgress(1 - elapsed / _cooldownDuration, false);
        }

        _view.SetProgress(0);
        _abilityCoroutine = null;
    }
}