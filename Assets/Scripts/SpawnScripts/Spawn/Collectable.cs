using System;
using UnityEngine;

[RequireComponent(typeof(Detected<>))]
public class Collectable<T1, T2> : MonoBehaviour, ISpawnable<T1> where T1 : MonoBehaviour, ISpawnable<T1> where T2 : MonoBehaviour
{
   private Detected<T2> _detected;

	public event Action<T1> ReadyToSpawn;

	private void Awake()
	{
		_detected = GetComponent<Detected<T2>>();
	}

	private void OnEnable()
	{
		_detected.WasDetected += () => ReadyToSpawn?.Invoke((T1)(object)this);
	}

	private void OnDisable()
	{
		_detected.WasDetected -= () => ReadyToSpawn?.Invoke((T1)(object)this);
	}
}