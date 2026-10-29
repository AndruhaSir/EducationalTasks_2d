using UnityEngine;

public class PlayerAttackAnimationEvent : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    public void OpenAttackHitbox()
    {
        _player?.OpenAttackHitbox();
    }

    public void CloseAttackHitbox()
    {
        _player?.CloseAttackHitbox();
    }
}
