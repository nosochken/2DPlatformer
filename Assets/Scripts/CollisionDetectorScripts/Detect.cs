using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsCustomizer<>))]
public class Detect<T> : MonoBehaviour, IDetectable where T : MonoBehaviour
{
    public event Action WasDetected;

    protected void OnWasDetected()
    {
        WasDetected?.Invoke();
    }

    public virtual void WasDetectedBy<T1>(Detector<T1> detector) where T1 : MonoBehaviour, IDetectable { }
}
