using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStander))]
public class PlayerRunner : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField, Min(1)] private float _runningSpeed = 10f;

    private PlayerStander _stander;

    private Rigidbody2D _rigidbody;

    private float _direction;

    private bool _shouldRun;

    public event Action<float> DirectionChanged;
    public event Action<bool> IsRunning;

    private void Awake()
    {
        _stander = GetComponent<PlayerStander>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputReader.DirectionChanged += direction => DirectionChanged?.Invoke(direction);
        _inputReader.DirectionChanged += ChangeRunningState;
    }

    private void OnDisable()
    {
        _inputReader.DirectionChanged -= direction => DirectionChanged?.Invoke(direction);
        _inputReader.DirectionChanged -= ChangeRunningState;
    }

    public void Run()
    {
        if (_direction != 0)
            PerformRun();
        else if (_shouldRun)
            StopRunning();
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
            StopRunning();
        }
    }

    private void PerformRun()
    {
        float x = _direction * _runningSpeed;
        Mover.MoveHorizontally(_rigidbody, x);
    }

    private void StopRunning()
    {
        _shouldRun = false;
        IsRunning?.Invoke(_shouldRun);
    }
}