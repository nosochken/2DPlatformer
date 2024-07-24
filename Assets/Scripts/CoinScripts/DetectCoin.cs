using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalCoin), typeof(CoinPhysicsCustomizer))]
public class DetectCoin : MonoBehaviour, IDetectable
{
    public event Action WasDetected;

    public void WasDetectedBy<T>(Detector<T> detector) where T : MonoBehaviour, IDetectable
    {
        if (detector.gameObject.TryGetComponent<Player>(out _) ||
        detector.gameObject.TryGetComponent<BottomBorder>(out _))
        {
            WasDetected?.Invoke();
        }
    }
}