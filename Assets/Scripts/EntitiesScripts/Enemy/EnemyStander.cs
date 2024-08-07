using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalEnemy), typeof(PlatformDetector))]
public class EnemyStander : MonoBehaviour
{
    private PlatformDetector _platformDetector;

    private bool _isOnPlatform;

    public event Action<bool> IsStanding;

    public bool IsOnPlatform => _isOnPlatform;

    private void Awake()
    {
        _platformDetector = GetComponent<PlatformDetector>();
    }

    private void OnEnable()
    {
        _platformDetector.Detected += TouchPlatform;
        _platformDetector.Missed += StopStanding;
    }

    private void OnDisable()
    {
        _platformDetector.Detected -= TouchPlatform;
        _platformDetector.Missed -= StopStanding;
    }

    private void TouchPlatform()
    {
        _isOnPlatform = true;
        IsStanding?.Invoke(_isOnPlatform);
    }

    private void StopStanding()
    {
        _isOnPlatform = false;
        IsStanding.Invoke(_isOnPlatform);
    }
}