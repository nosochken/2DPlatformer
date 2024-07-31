using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationsChanger), typeof(PhysicalPlayer), typeof(PlayerPhysicsCustomizer))]
[RequireComponent(typeof(PlayerMover), typeof(DetectPlayer), typeof(EnemyAttacker))]
[RequireComponent(typeof(PlayerCoinCollector), typeof(AttackedPlayer), typeof(HealablePlayer))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, ILivable
{
    private PlayerMover _mover;
    private EnemyAttacker _enemyAttacker;

    public event Action<bool> IsStanding;
    public event Action<float> ChangedDirection;
    public event Action<bool> IsRunning;
    public event Action<bool> IsJumping;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
    }

    private void OnEnable()
    {
        _mover.IsStanding += isStanding => IsStanding.Invoke(isStanding);
        _mover.ChangedDirection += direction => ChangedDirection?.Invoke(direction);
        _mover.IsRunning += isRunning => IsRunning?.Invoke(isRunning);
        _mover.IsJumping += isJumping => IsJumping?.Invoke(isJumping);
    }

    private void FixedUpdate()
    {
        _mover.Move();
        _enemyAttacker.Attack();
    }

    private void OnDisable()
    {
        _mover.IsStanding -= isStanding => IsStanding.Invoke(isStanding);
        _mover.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
        _mover.IsRunning -= isRunning => IsRunning?.Invoke(isRunning);
        _mover.IsJumping -= isJumping => IsJumping?.Invoke(isJumping);
    }
}