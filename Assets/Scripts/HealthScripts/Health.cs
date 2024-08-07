using System;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Health<T> : MonoBehaviour where T : MonoBehaviour
{
	[SerializeField] private float _maxValue = 100f;
	[SerializeField, Min(0.1f)] private float _colorChangeDelay = 0.5f;
	
	private ColorChanger _colorChanger;
	private WaitForSeconds _waitForSeconds;

	private float _minValue = 0f;
	private float _currentValue;
	
	private bool _isDead;
	
	public event Action Died;
	
	public bool IsDead => _isDead;

	private void Awake()
	{
		_colorChanger = GetComponent<ColorChanger>();
		_waitForSeconds = new WaitForSeconds(_colorChangeDelay);
	}
	
	private void OnEnable()
	{
		Restore();
	}

	public void Decrease(float value)
	{
		if (value <= 0)
			return;

		_currentValue -= value;

		TurnRedFromDamage();
		
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
		
		TurnGreenFromRecovery();

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

	private void TurnRedFromDamage()
	{
		StartCoroutine(_colorChanger.ChangeColorForWhile(_waitForSeconds, Color.red));
	}
	
	private void TurnGreenFromRecovery()
	{
		StartCoroutine(_colorChanger.ChangeColorForWhile(_waitForSeconds, Color.green));
	}
}