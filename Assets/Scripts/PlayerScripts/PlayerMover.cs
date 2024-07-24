using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStander), typeof(PlayerRunner), typeof(PlayerJumper))]
public class PlayerMover : MonoBehaviour
{
    private PlayerStander _stander;
    private PlayerRunner _runner;
    private PlayerJumper _jumper;

    public event Action<bool> IsStanding;
    public event Action<float> ChangedDirection;
    public event Action<bool> IsRunning;
    public event Action<bool> IsJumping;

    private void Awake()
    {
        _stander = GetComponent<PlayerStander>();
        _runner = GetComponent<PlayerRunner>();
        _jumper = GetComponent<PlayerJumper>();
    }
    private void OnEnable()
    {
        _stander.IsStanding += isStanding => IsStanding.Invoke(isStanding);
        _runner.DirectionChanged += direction => ChangedDirection?.Invoke(direction);
        _runner.IsRunning += isRunning => IsRunning?.Invoke(isRunning);
        _jumper.IsJumping += isJumping => IsJumping?.Invoke(isJumping);
    }

    private void OnDisable()
    {
        _stander.IsStanding -= isStanding => IsStanding.Invoke(isStanding);
        _runner.DirectionChanged -= direction => ChangedDirection?.Invoke(direction);
        _runner.IsRunning -= isRunning => IsRunning?.Invoke(isRunning);
        _jumper.IsJumping -= isJumping => IsJumping?.Invoke(isJumping);
    }

    public void Move()
    {
        _runner.Run();
        _jumper.Jump();
    }
}