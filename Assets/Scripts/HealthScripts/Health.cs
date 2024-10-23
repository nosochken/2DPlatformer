using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 100f;

    private float _minValue = 0f;
    private float _currentValue;
    private bool _isDead;

    public event Action<float, float> Changed;
    public event Action Died;

    public float CurrentValue => _currentValue;
    public float MaxValue => _maxValue;
    public bool IsDead => _isDead;

    private void OnEnable()
    {
        Restore();
    }

    public void Decrease(float value)
    {
        if (value <= 0)
            return;

        Change(-value);

        TryDie();
    }

    public void Increase(float value)
    {
        if (value <= 0)
            return;

        Change(value);
    }

    protected virtual void TryDie()
    {
        if (_currentValue <= _minValue)
        {
            _isDead = true;
            Died?.Invoke();
        }
    }

    protected void Restore()
    {
        _isDead = false;
        _currentValue = _maxValue;

        Changed?.Invoke(_currentValue, _maxValue);
    }

    private void Change(float value)
    {
        _currentValue = Mathf.Clamp(_currentValue + value, _minValue, _maxValue);

        Changed?.Invoke(_currentValue, _maxValue);
    }
}