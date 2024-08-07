using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsCustomizer<>))]
public class Detection<T> : MonoBehaviour, IDetectable where T : MonoBehaviour
{
    public event Action WasDetected;

    public virtual void WasDetectedBy<T1>(Detector<T1> detector) where T1 : MonoBehaviour, IDetectable
    {
        if (detector.gameObject.TryGetComponent<BottomBorder>(out _) ||
        detector.gameObject.TryGetComponent<Player>(out _))
        {
            WasDetected?.Invoke();
        }
    }
}