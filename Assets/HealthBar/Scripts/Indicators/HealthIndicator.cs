using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Changed += Display;

        Display(_health.CurrentValue, _health.MaxValue);
    }

    private void OnDisable()
    {
        _health.Changed -= Display;
    }

    protected abstract void Display(float currentHealth, float maxHealth);
}