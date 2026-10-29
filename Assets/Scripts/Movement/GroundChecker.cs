using UnityEngine;

public class GroundChecker
{
    private readonly Transform _transform;
    private readonly float _radius;
    private readonly LayerMask _groundLayer;

    public GroundChecker(Transform transform, float radius, LayerMask groundLayer)
    {
        _transform = transform;
        _radius = radius;
        _groundLayer = groundLayer;
    }

    public bool Check()
    {
        Vector2 checkPosition = (Vector2)_transform.position;

        return Physics2D.OverlapCircle(checkPosition, _radius, _groundLayer);
    }

    public void DrawGizmos()
    {
        if (_transform == null) return;

        Vector2 checkPosition = (Vector2)_transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPosition, _radius);
    }
}