using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationsChanger), typeof(PhysicalEnemy), typeof(EnemyPhysicsCustomizer))]
[RequireComponent(typeof(PatrollingEnemy), typeof(SpawnEnemy), typeof(DetectEnemy))]
public class Enemy : MonoBehaviour
{
    private PatrollingEnemy _patrollingEnemy;

    public event Action<bool> IsStanding;
    public event Action<bool> IsWalking;
    public event Action<float> ChangedDirection;

    private void Awake()
    {
        _patrollingEnemy = GetComponent<PatrollingEnemy>();
    }

    private void OnEnable()
    {
        _patrollingEnemy.IsStanding += isStanding => IsStanding?.Invoke(isStanding);
        _patrollingEnemy.IsWalking += isWalking => IsWalking?.Invoke(isWalking);
        _patrollingEnemy.ChangedDirection += direction => ChangedDirection?.Invoke(direction);
    }

    private void FixedUpdate()
    {
        _patrollingEnemy.TryPatrol();
    }

    private void OnDisable()
    {
        _patrollingEnemy.IsStanding -= isStanding => IsStanding?.Invoke(isStanding);
        _patrollingEnemy.IsWalking -= isWalking => IsWalking?.Invoke(isWalking);
        _patrollingEnemy.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
    }
}