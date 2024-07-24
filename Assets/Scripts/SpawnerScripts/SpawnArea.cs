using System.Collections.Generic;
using UnityEngine;

public class SpawnArea<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T _prefab;

    [SerializeField] private Platform _platform;
    [SerializeField] private LeftBorder _leftBorder;
    [SerializeField] private RightBorder _rightBorder;

    private List<SpawnZone> _spawnZones;

    private float _closestXToSceneLeftBorder;
    private float _closestXToSceneRightBorder;

    private void Awake()
    {
        _spawnZones = new List<SpawnZone>();
    }

    private void Start()
    {
        FindClosestXToSceneOnBorder(_leftBorder);
        FindClosestXToSceneOnBorder(_rightBorder);

        FindAllSpawnZones();
    }

    public SpawnZone GetSpawnZone()
    {
        int startIndex = 0;

        return _spawnZones[Random.Range(startIndex, _spawnZones.Count)];
    }

    private void FindAllSpawnZones()
    {
        foreach (Vector2[] points in _platform.PointsOfParts)
            FindSpawnZone(points);
    }

    private void FindSpawnZone(Vector2[] points)
    {
        Vector2 lastPoint = Vector2.zero;
        float permissibleDeviation = 0.01f;

        for (int i = 0; i < points.Length; i++)
        {
            if (Mathf.Abs(points[i].y - lastPoint.y) < permissibleDeviation)
            {
                float leftX = points[i].x;
                float rightX = lastPoint.x;

                TryDetectCrossingWithLeftBorder(ref leftX);
                TryDetectCrossingWithRightBorder(ref rightX);

                if (TryPut(leftX, rightX))
                    CreateSpawnZone(leftX, rightX, lastPoint.y);
            }

            lastPoint = points[i];
        }
    }

    private bool TryPut(float leftX, float rightX)
    {
        float segmentLength = Mathf.Abs(leftX - rightX);

        if (_prefab.TryGetComponent(out CapsuleCollider2D prefabCollider))
        {
            if (prefabCollider.size.x < segmentLength)
                return true;
        }

        return false;
    }

    private void CreateSpawnZone(float leftX, float rightX, float y)
    {
        SpawnZone spawnZone = new SpawnZone();
        spawnZone.Initialize(leftX, rightX, y);

        _spawnZones.Add(spawnZone);
    }

    private void TryDetectCrossingWithLeftBorder(ref float leftX)
    {
        if (leftX < _closestXToSceneLeftBorder)
            leftX = _closestXToSceneLeftBorder;
    }

    private void TryDetectCrossingWithRightBorder(ref float rightX)
    {
        if (rightX > _closestXToSceneRightBorder)
            rightX = _closestXToSceneRightBorder;
    }

    private void FindClosestXToSceneOnBorder(Border border)
    {
        border.TryGetComponent(out Collider2D borderCollider);

        if (border is LeftBorder)
            _closestXToSceneLeftBorder = borderCollider.bounds.max.x;
        else if (border is RightBorder)
            _closestXToSceneRightBorder = borderCollider.bounds.min.x;
    }
}