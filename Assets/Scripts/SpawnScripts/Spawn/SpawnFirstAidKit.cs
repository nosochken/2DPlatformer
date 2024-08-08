using System;
using UnityEngine;

[RequireComponent(typeof(DetectFirstAidKit))]
public class SpawnFirstAidKit : Collectable<SpawnFirstAidKit, FirstAidKit>
{
    /*private DetectFirstAidKit _detectFirstAidKit;

    public event Action<SpawnFirstAidKit> ReadyToSpawn;

    private void Awake()
    {
        _detectFirstAidKit = GetComponent<DetectFirstAidKit>();
    }

    private void OnEnable()
    {
        _detectFirstAidKit.WasDetected += () => ReadyToSpawn?.Invoke(this);
    }

    private void OnDisable()
    {
        _detectFirstAidKit.WasDetected -= () => ReadyToSpawn?.Invoke(this);
    }*/
}