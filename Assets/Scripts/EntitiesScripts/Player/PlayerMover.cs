using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStander), typeof(PlayerRunner), typeof(PlayerJumper))]
[RequireComponent(typeof(AttackedPlayer))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private PlayerStander _stander;
    private PlayerRunner _runner;
    private PlayerJumper _jumper;
    private AttackedPlayer _attacked;

    private float _direction;

    private bool _shouldRun;
    private bool _shouldJump;

    public event Action<bool> IsStanding;
    public event Action<float> DirectionChanged;
    public event Action<bool> IsRunning;
    public event Action<bool> IsJumping;

    private void Awake()
    {
        _stander = GetComponent<PlayerStander>();
        _runner = GetComponent<PlayerRunner>();
        _jumper = GetComponent<PlayerJumper>();
        _attacked = GetComponent<AttackedPlayer>();
    }
    private void OnEnable()
    {
        _inputReader.DirectionChanged += direction => DirectionChanged?.Invoke(direction);
        _inputReader.DirectionChanged += ChangeRunningState;
        _inputReader.JumpKeyPressed += TryChangeJumpingState;

        _stander.IsStanding += isStanding => IsStanding.Invoke(isStanding);
        _runner.IsRunning += isRunning => IsRunning?.Invoke(isRunning);
        _jumper.IsJumping += isJumping => IsJumping?.Invoke(isJumping);
    }

    private void OnDisable()
    {
        _inputReader.DirectionChanged -= direction => DirectionChanged?.Invoke(direction);
        _inputReader.DirectionChanged -= ChangeRunningState;
        _inputReader.JumpKeyPressed -= TryChangeJumpingState;

        _stander.IsStanding -= isStanding => IsStanding.Invoke(isStanding);
        _runner.IsRunning -= isRunning => IsRunning?.Invoke(isRunning);
        _jumper.IsJumping -= isJumping => IsJumping?.Invoke(isJumping);
    }

    public void FixedUpdate()
    {
        if (_attacked.CanMove)
            Move();
        Jump();
    }

    private void Move()
    {
        if (_direction != 0)
        {
            _runner.Run(_direction);
        }
        else if (_shouldRun)
        {
            _shouldRun = false;
            _runner.StopRunning();
        }
    }

    private void Jump()
    {
        if (_shouldJump)
        {
            StartCoroutine(_jumper.Jump());
            _shouldJump = false;
        }
    }

    private void ChangeRunningState(float direction)
    {
        _direction = direction;

        if (_stander.IsOnPlatform)
        {
            _shouldRun = true;
            IsRunning?.Invoke(_shouldRun);
        }
        else
        {
            _runner.StopRunning();
        }
    }

    private void TryChangeJumpingState()
    {
        if (_stander.IsOnPlatform)
            _shouldJump = true;
    }
}