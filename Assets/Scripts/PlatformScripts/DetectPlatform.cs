using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalPlatform), typeof(PlatformPhysicsCustomizer))]
public class DetectPlatform : MonoBehaviour, IDetectable
{
    public event Action WasDetected;

    public void WasDetectedBy<T>(Detector<T> detector) where T : MonoBehaviour, IDetectable { }
}