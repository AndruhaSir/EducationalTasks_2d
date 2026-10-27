using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private bool _startDirectionRight = true;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flip(float horizontalDirection)
    {
        if (Mathf.Approximately(horizontalDirection, 0f)) return;

        if (_startDirectionRight)
        {
            _spriteRenderer.flipX = horizontalDirection < 0f;
        }
        else
        {
            _spriteRenderer.flipX = horizontalDirection > 0f;
        }
    }
}