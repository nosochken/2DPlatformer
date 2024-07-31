using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationsChanger), typeof(PhysicalEnemy), typeof(EnemyPhysicsCustomizer))]
[RequireComponent(typeof(PatrollingEnemy), typeof(SpawnEnemy), typeof(DetectEnemy))]
[RequireComponent(typeof(EnemyHealth), typeof(AttackedEnemy), typeof(PlayerAttacker))]
public class Enemy : MonoBehaviour, ILivable
{
    private PatrollingEnemy _patrollingEnemy;
    private PlayerAttacker _playerAttacker;

    public event Action<bool> IsStanding;
    public event Action<bool> IsWalking;
    public event Action<float> ChangedDirection;

    private void Awake()
    {
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
        _playerAttacker.Attack();
    }

    private void OnDisable()
    {
        _patrollingEnemy.IsStanding -= isStanding => IsStanding?.Invoke(isStanding);
        _patrollingEnemy.IsWalking -= isWalking => IsWalking?.Invoke(isWalking);
        _patrollingEnemy.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
    }
}