using System;
using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationsChanger), typeof(PhysicalPlayer), typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMover), typeof(DetectPlayer), typeof(AttackerOfEnemy))]
[RequireComponent(typeof(PlayerCoinCollector), typeof(AttackedPlayer), typeof(HealablePlayer))]
public class Player : MonoBehaviour, ILivable
{
    [SerializeField] private InputReader _inputReader;

    private PlayerMover _mover;
    private Vampirism _vampirism;

    public event Action<bool> IsStanding;
    public event Action<float> ChangedDirection;
    public event Action<bool> IsRunning;
    public event Action<bool> IsJumping;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _vampirism = GetComponentInChildren<Vampirism>();
    }

    private void OnEnable()
    {
        _mover.IsStanding += isStanding => IsStanding.Invoke(isStanding);
        _mover.DirectionChanged += direction => ChangedDirection?.Invoke(direction);
        _mover.IsRunning += isRunning => IsRunning?.Invoke(isRunning);
        _mover.IsJumping += isJumping => IsJumping?.Invoke(isJumping);

        _inputReader.VampirismKeyPressed += () => _vampirism.Activate();
    }

    private void OnDisable()
    {
        _mover.IsStanding -= isStanding => IsStanding.Invoke(isStanding);
        _mover.DirectionChanged -= direction => ChangedDirection?.Invoke(direction);
        _mover.IsRunning -= isRunning => IsRunning?.Invoke(isRunning);
        _mover.IsJumping -= isJumping => IsJumping?.Invoke(isJumping);

        _inputReader.VampirismKeyPressed -= () => _vampirism.Activate();
    }
}