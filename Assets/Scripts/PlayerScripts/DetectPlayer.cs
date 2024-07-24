using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalPlayer), typeof(PlayerPhysicsCustomizer))]
public class DetectPlayer : MonoBehaviour, IDetectable
{
    private Vector2 _startPosition;

    public event Action WasDetected;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void WasDetectedBy<T>(Detector<T> detector) where T : MonoBehaviour, IDetectable
    {
        if (detector.gameObject.TryGetComponent<BottomBorder>(out _))
            transform.position = _startPosition;
    }
}