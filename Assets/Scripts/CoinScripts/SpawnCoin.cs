using System;
using UnityEngine;

[RequireComponent(typeof(DetectCoin))]
public class SpawnCoin : MonoBehaviour, ISpawnable
{
    private DetectCoin _detectCoin;

    public event Action<ISpawnable> ReadyToSpawn;

    private void Awake()
    {
        _detectCoin = GetComponent<DetectCoin>();
    }

    private void OnEnable()
    {
        _detectCoin.WasDetected += () => ReadyToSpawn?.Invoke(this);
    }

    private void OnDisable()
    {
        _detectCoin.WasDetected -= () => ReadyToSpawn?.Invoke(this);
    }
}