using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalPlayer), typeof(PlatformDetector))]
public class PlayerStander : MonoBehaviour
{
    private CapsuleCollider2D _collider;

    private PlatformDetector _platformDetector;

    private bool _isOnPlatform;

    public event Action<bool> IsStanding;

    public bool IsOnPlatform => _isOnPlatform;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();

        _platformDetector = GetComponent<PlatformDetector>();
    }

    private void OnEnable()
    {
        _platformDetector.DetectedWithCollisionInformation += Stand;
        _platformDetector.Missed += StopStanding;
    }

    private void OnDisable()
    {
        _platformDetector.DetectedWithCollisionInformation -= Stand;
        _platformDetector.Missed -= StopStanding;
    }

    private void Stand(Collision2D collision)
    {
        float lowestYPoint = Mathf.Floor(_collider.bounds.min.y);

        int firstContactIndex = 0;
        float contactPointY = Mathf.Floor(collision.GetContact(firstContactIndex).point.y);

        if (contactPointY == lowestYPoint)
        {
            _isOnPlatform = true;
            IsStanding.Invoke(_isOnPlatform);
        }
    }

    private void StopStanding()
    {
        _isOnPlatform = false;
        IsStanding.Invoke(_isOnPlatform);
    }
}