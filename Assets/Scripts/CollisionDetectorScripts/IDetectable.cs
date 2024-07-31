using System;
using UnityEngine;

public interface IDetectable
{
    public event Action WasDetected;

    public virtual void WasDetectedBy<T>(Detector<T> detector) where T : MonoBehaviour, IDetectable { }

}