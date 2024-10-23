using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationsChanger), typeof(PhysicalEnemy), typeof(EnemyHealth))]
[RequireComponent(typeof(PatrollingEnemy), typeof(SpawnEnemy), typeof(DetectEnemy))]
[RequireComponent(typeof(AttackedEnemy), typeof(PlayerStalker), typeof(AttackerOfPlayer))]
public class Enemy : MonoBehaviour, ILivable
{
    private PatrollingEnemy _patrollingEnemy;
    private PlayerStalker _playerStalker;

    public event Action<bool> IsStanding;
    public event Action<bool> IsWalking;
    public event Action<float> ChangedDirection;

    private void Awake()
    {
        _playerStalker = GetComponent<PlayerStalker>();
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
        if (_patrollingEnemy.CanPatrol && !_playerStalker.IsStalking)
            _patrollingEnemy.Patrol();

        if (_playerStalker.CanStalk())
            _playerStalker.Stalk();
    }

    private void OnDisable()
    {
        _patrollingEnemy.IsStanding -= isStanding => IsStanding?.Invoke(isStanding);
        _patrollingEnemy.IsWalking -= isWalking => IsWalking?.Invoke(isWalking);
        _patrollingEnemy.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
    }
}