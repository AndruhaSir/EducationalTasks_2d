using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();

    public int Count => _waypoints.Count;

    public int GetClosestPointIndex(Vector3 position)
    {
        if (Count == 0) return 0;

        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < Count; i++)
        {
            float sqrDistance = (position - _waypoints[i].position).sqrMagnitude;

            if (sqrDistance < closestDistance)
            {
                closestDistance = sqrDistance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    public Vector3 GetPointPosition(int index)
    {
        if (Count == 0) return transform.position;

        // Защита от выхода за границы (зацикливаем)
        index = ((index % Count) + Count) % Count;
        return _waypoints[index].position;
    }
}