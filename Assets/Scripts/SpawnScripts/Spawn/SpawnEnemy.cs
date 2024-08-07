using System;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth), typeof(DetectEnemy))]
public class SpawnEnemy : MonoBehaviour, ISpawnable<SpawnEnemy>
{
	private EnemyHealth _health;
	private DetectEnemy _detectEnemy;

	public event Action<SpawnEnemy> ReadyToSpawn;
	public event Action<SpawnZone> GotSpawnZone;

	private void Awake()
	{
		_detectEnemy = GetComponent<DetectEnemy>();
		_health = GetComponent<EnemyHealth>();
	}

	private void OnEnable()
	{
		_health.Died += () => ReadyToSpawn?.Invoke(this);
		_detectEnemy.WasDetected += () => ReadyToSpawn?.Invoke(this);
	}

	private void OnDisable()
	{
		_health.Died -= () => ReadyToSpawn?.Invoke(this);
		_detectEnemy.WasDetected -= () => ReadyToSpawn?.Invoke(this);
	}

	public void GetSpawnZone(SpawnZone spawnZone)
	{
		GotSpawnZone?.Invoke(spawnZone);
	}
}