using System;
using UnityEngine;

public class Detector<T> : MonoBehaviour where T : MonoBehaviour, IDetectable
{
    public event Action Detected;
    public event Action<T> DetectedWitDetected;
    public event Action<Collision2D> DetectedWithCollisionInformation;

    public event Action Missed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T detection))
        {
            Detected?.Invoke();
            DetectedWitDetected?.Invoke(detection);
            DetectedWithCollisionInformation?.Invoke(collision);

            detection.WasDetectedBy(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T detection))
            Missed?.Invoke();
    }
}