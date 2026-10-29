using UnityEngine;

public class EnemyAttackAnimationEvent : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public void OpenAttackHitbox()
    {
        _enemy?.OpenAttackHitbox();
    }

    public void CloseAttackHitbox()
    {
        _enemy?.CloseAttackHitbox();
    }
}
