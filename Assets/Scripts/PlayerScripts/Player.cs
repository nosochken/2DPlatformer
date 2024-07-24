using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationsChanger), typeof(PhysicalPlayer), typeof(PlayerPhysicsCustomizer))]
[RequireComponent(typeof(PlayerMover), typeof(DetectPlayer))]
[RequireComponent(typeof(PlayerCoinCollector), typeof(EnemyDetector))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;

    public event Action<bool> IsStanding;
    public event Action<float> ChangedDirection;
    public event Action<bool> IsRunning;
    public event Action<bool> IsJumping;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
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
        _mover.TryMove();
    }

    private void OnDisable()
    {
        _mover.IsStanding -= isStanding => IsStanding.Invoke(isStanding);
        _mover.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
        _mover.IsRunning -= isRunning => IsRunning?.Invoke(isRunning);
        _mover.IsJumping -= isJumping => IsJumping?.Invoke(isJumping);
    }
}