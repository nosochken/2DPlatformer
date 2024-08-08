using System;
using UnityEngine;

[RequireComponent(typeof(SpawnEnemy), typeof(EnemyMover), typeof(PlayerStalker))]
public class PatrollingEnemy : MonoBehaviour
{
	private SpawnEnemy _spawnEnemy;
	private EnemyMover _mover;
	private SpawnZone _patrolZone;

	private bool _canPatrol;

	public event Action<bool> IsStanding;
	public event Action<bool> IsWalking;
	public event Action<float> ChangedDirection;
	
	public bool CanPatrol => _canPatrol;

	private void Awake()
	{
		_spawnEnemy = GetComponent<SpawnEnemy>();
		_mover = GetComponent<EnemyMover>();
	}

	private void OnEnable()
	{
		_spawnEnemy.GotSpawnZone += GetPatrolZone;

		_mover.IsStanding += isStanding => IsStanding?.Invoke(isStanding);
		_mover.IsWalking += isWalking => IsWalking?.Invoke(isWalking);
		_mover.ChangedDirection += direction => ChangedDirection?.Invoke(direction);
	}

	private void OnDisable()
	{
		_spawnEnemy.GotSpawnZone -= GetPatrolZone;

		_mover.IsStanding -= isStanding => IsStanding?.Invoke(isStanding);
		_mover.IsWalking -= isWalking => IsWalking?.Invoke(isWalking);
		_mover.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);

		_canPatrol = false;
	}

	public void Patrol()
	{
		_mover.Move(_patrolZone.LeftmostX, _patrolZone.RightmostX);
	}

	private void GetPatrolZone(SpawnZone patrolZone)
	{
		_patrolZone = patrolZone;
		_canPatrol = true;
	}
}