using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour, ISpawnable<SpawnEnemy>
{
    private DetectEnemy _detectEnemy;

    public event Action<SpawnEnemy> ReadyToSpawn;
    public event Action<SpawnZone> GotSpawnZone;

    private void Awake()
    {
        _detectEnemy = GetComponent<DetectEnemy>();
    }

    private void OnEnable()
    {
        _detectEnemy.WasDetected += () => ReadyToSpawn?.Invoke(this);
    }

    private void OnDisable()
    {
        _detectEnemy.WasDetected -= () => ReadyToSpawn?.Invoke(this);
    }

    public void GetSpawnZone(SpawnZone spawnZone)
    {
        GotSpawnZone?.Invoke(spawnZone);
    }
}