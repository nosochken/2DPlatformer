using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalEnemy), typeof(EnemyPhysicsCustomizer))]
public class DetectEnemy : MonoBehaviour, IDetectable
{
    public event Action WasDetected;

    public void WasDetectedBy<T>(Detector<T> detector) where T : MonoBehaviour, IDetectable
    {
        if (detector.gameObject.TryGetComponent<BottomBorder>(out _) ||
        detector.gameObject.TryGetComponent<Player>(out _))
        {
            WasDetected?.Invoke();
        }
    }
}