using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStander))]
public class PlayerRunner : MonoBehaviour
{
    [SerializeField, Min(1)] private float _runningSpeed = 10f;

    private Rigidbody2D _rigidbody;

    public event Action<bool> IsRunning;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Run(float direction)
    {
        float x = direction * _runningSpeed;
        Mover.MoveHorizontally(_rigidbody, x);
    }

    public void StopRunning()
    {
        IsRunning?.Invoke(false);
    }
}