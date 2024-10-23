using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(FirstAidKitDetector))]
public class HealablePlayer : MonoBehaviour
{
    private PlayerHealth _health;
    private FirstAidKitDetector _firstAidKitDetector;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _firstAidKitDetector = GetComponent<FirstAidKitDetector>();
    }

    private void OnEnable()
    {
        _firstAidKitDetector.DetectedWitDetected += Heal;
    }

    private void OnDisable()
    {
        _firstAidKitDetector.DetectedWitDetected -= Heal;
    }

    private void Heal(DetectFirstAidKit detectFirstAidKit)
    {
        _health.Increase(detectFirstAidKit.HealingValue);
    }
}