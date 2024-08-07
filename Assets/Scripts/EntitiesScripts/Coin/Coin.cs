using System;
using UnityEngine;

[RequireComponent(typeof(CoinAnimationsChanger), typeof(PhysicalCoin), typeof(DetectCoin))]
[RequireComponent(typeof(CoinMover), typeof(SpawnCoin))]
public class Coin : MonoBehaviour
{
    private CoinMover _mover;

    public event Action<bool> IsShining;

    private void Awake()
    {
        _mover = GetComponent<CoinMover>();
    }

    private void OnEnable()
    {
        _mover.ReachedHighestYPoint += isShining => IsShining.Invoke(isShining);
    }

    private void FixedUpdate()
    {
        _mover.Move();
    }

    private void OnDisable()
    {
        _mover.ReachedHighestYPoint -= isShining => IsShining.Invoke(isShining);
    }
}