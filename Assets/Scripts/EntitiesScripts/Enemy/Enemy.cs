using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationsChanger), typeof(PhysicalEnemy), typeof(EnemyHealth))]
[RequireComponent(typeof(PatrollingEnemy), typeof(SpawnEnemy), typeof(DetectEnemy))]
[RequireComponent(typeof(AttackedEnemy), typeof(PlayerStalker), typeof(PlayerAttacker))]
public class Enemy : MonoBehaviour, ILivable
{
	private PatrollingEnemy _patrollingEnemy;
	private PlayerStalker _playerStalker;
	private PlayerAttacker _playerAttacker;

	public event Action<bool> IsStanding;
	public event Action<bool> IsWalking;
	public event Action<float> ChangedDirection;

	private void Awake()
	{
		_playerStalker = GetComponent<PlayerStalker>();
		_patrollingEnemy = GetComponent<PatrollingEnemy>();
		_playerAttacker = GetComponent<PlayerAttacker>();
	}

	private void OnEnable()
	{
		_patrollingEnemy.IsStanding += isStanding => IsStanding?.Invoke(isStanding);
		_patrollingEnemy.IsWalking += isWalking => IsWalking?.Invoke(isWalking);
		_patrollingEnemy.ChangedDirection += direction => ChangedDirection?.Invoke(direction);
	}

	private void FixedUpdate()
	{
		_patrollingEnemy.Patrol();
		_playerStalker.Stalk();
		_playerAttacker.Attack();
	}

	private void OnDisable()
	{
		_patrollingEnemy.IsStanding -= isStanding => IsStanding?.Invoke(isStanding);
		_patrollingEnemy.IsWalking -= isWalking => IsWalking?.Invoke(isWalking);
		_patrollingEnemy.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
	}
}