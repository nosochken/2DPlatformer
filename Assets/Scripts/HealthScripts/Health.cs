using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private float _maxValue = 100f;

	private float _minValue = 0f;
	private float _currentValue;

	private bool _isDead;
	
	public event Action Increased;
	public event Action Decreased;
	public event Action Died;

	public bool IsDead => _isDead;

	private void OnEnable()
	{
		Restore();
	}

	public void Decrease(float value)
	{
		if (value <= 0)
			return;

		_currentValue -= value;

		Decreased?.Invoke();

		TryIncreaseToMinimumValue();

		TryDie();
	}

	protected virtual void TryDie()
	{
		if (_currentValue <= _minValue)
		{
			_isDead = true;
			Died?.Invoke();
		}
	}

	protected void Increase(float value)
	{
		if (value <= 0)
			return;

		_currentValue += value;

		Increased?.Invoke();

		TryReduceToMaxValue();
	}

	protected void Restore()
	{
		_isDead = false;
		_currentValue = _maxValue;
	}

	private void TryIncreaseToMinimumValue()
	{
		if (_currentValue < _minValue)
			_currentValue = _minValue;
	}

	private void TryReduceToMaxValue()
	{
		if (_currentValue > _maxValue)
			_currentValue = _maxValue;
	}
}