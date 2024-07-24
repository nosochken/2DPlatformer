using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicalPlatform), typeof(PlatformPhysicsCustomizer), typeof(DetectPlatform))]
public class Platform : MonoBehaviour
{
    private CompositeCollider2D _compositeCollider;

    private List<Vector2[]> _pointsOfParts;

    private int _partsAmount;

    public IEnumerable PointsOfParts => _pointsOfParts;

    private void Awake()
    {
        _compositeCollider = GetComponent<CompositeCollider2D>();

        _partsAmount = _compositeCollider.pathCount;
        _pointsOfParts = new List<Vector2[]>();

        FindAllPointsOfParts();
    }

    private void FindAllPointsOfParts()
    {
        for (int i = 0; i < _partsAmount; i++)
        {
            Vector2[] points = new Vector2[_compositeCollider.GetPathPointCount(i)];
            _compositeCollider.GetPath(i, points);

            _pointsOfParts.Add(points);
        }
    }
}