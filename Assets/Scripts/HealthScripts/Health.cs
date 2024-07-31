using System;
using System.Collections;
using UnityEngine;

public class Health<T> : MonoBehaviour where T : MonoBehaviour
{
	[SerializeField, Min(0.1f)] private float _damageColorDelay = 0.5f;
	protected float CurrentValue;
	
	private SpriteRenderer _spriteRenderer;
	private Color _defaultColor;
	private WaitForSeconds _waitForSeconds;
	
	private float _maxValue;
	private float _minValue = 0f;
	
	public Action Died;
	
	protected float MaxValue => _maxValue;
	
	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_defaultColor = _spriteRenderer.color;
		
		_waitForSeconds = new WaitForSeconds(_damageColorDelay);
	}
	
	public void Decrease(float value)
	{
		if (value <= 0)
			return;
		
		CurrentValue -= value;
		
		StartCoroutine(TurnRedFromDamage());
		
		TryDie();
	}
	
	protected void Initialize(float maxValue)
	{
		if (maxValue < _minValue)
			return;
			
		_maxValue = maxValue;
		CurrentValue = _maxValue;
	}
	
	private IEnumerator TurnRedFromDamage()
	{
		_spriteRenderer.color = Color.red;
		yield return _waitForSeconds;
		
		_spriteRenderer.color = _defaultColor;
	}
	
	private void TryDie()
	{
		if (CurrentValue <= _maxValue)
			Died?.Invoke();
	}
}