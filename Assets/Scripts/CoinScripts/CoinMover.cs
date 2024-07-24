using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PhysicalCoin), typeof(CoinPhysicsCustomizer), typeof(PlatformDetector))]
public class CoinMover : MonoBehaviour
{
    [SerializeField, Min(1)] private float _maxLiftHeight = 1f;
    [SerializeField, Min(1)] private float _movementSpeed = 50f;

    private Rigidbody2D _rigidbody;

    private PlatformDetector _platformDetector;

    private WaitForFixedUpdate _waitForFixedUpdate;

    private float _highestYPoint;

    private int _oppositeMovement = -1;

    private bool _didTouchPlatform;
    private bool _didReachHighestYPoint;

    public event Action<bool> ReachedHighestYPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _platformDetector = GetComponent<PlatformDetector>();

        _waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        _platformDetector.Detected += TouchPlatform;
    }

    private void OnDisable()
    {
        _platformDetector.Detected -= TouchPlatform;
    }

    public void Move()
    {
        if (_didTouchPlatform)
            StartCoroutine(MoveUp());
        else
            StartCoroutine(MoveDown());
    }

    private IEnumerator MoveUp()
    {
        while (transform.position.y < _highestYPoint)
        {
            Mover.MoveVertically(_rigidbody, _movementSpeed);

            yield return _waitForFixedUpdate;
        }

        _didReachHighestYPoint = true;
        ReachedHighestYPoint?.Invoke(_didReachHighestYPoint);

        _didTouchPlatform = false;
    }

    private IEnumerator MoveDown()
    {
        _didReachHighestYPoint = false;
        ReachedHighestYPoint?.Invoke(_didReachHighestYPoint);

        float y = _movementSpeed * _oppositeMovement;

        while (!_didTouchPlatform)
        {
            Mover.MoveVertically(_rigidbody, y);

            yield return _waitForFixedUpdate;
        }
    }

    private void TouchPlatform()
    {
        _didTouchPlatform = true;
        _highestYPoint = transform.position.y + _maxLiftHeight;
    }
}