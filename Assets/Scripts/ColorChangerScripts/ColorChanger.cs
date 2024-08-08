using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Health))]
public class ColorChanger : MonoBehaviour
{
	[SerializeField, Min(0.1f)] private float _delay = 0.5f;
	
	private SpriteRenderer _spriteRenderer;
	private Color _defaultColor;
	
	private WaitForSeconds _waitForSeconds;
	
	private Health _health;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_defaultColor = _spriteRenderer.color;
		
		_waitForSeconds = new WaitForSeconds(_delay);
		
		_health = GetComponent<Health>();
	}

	private void OnEnable()
	{
		_spriteRenderer.color = _defaultColor;
		
		_health.Increased += TurnGreen;
		_health.Decreased += TurnRed;
	}
	
	private void OnDisable()
	{
		_health.Increased -= TurnGreen;
		_health.Decreased -= TurnRed;
	}
	
	private void TurnGreen()
	{
		StartCoroutine(ChangeColorForWhile(_waitForSeconds, Color.green));
	}
	
	private void TurnRed()
	{
		StartCoroutine(ChangeColorForWhile(_waitForSeconds, Color.red));
	}

	private IEnumerator ChangeColorForWhile(WaitForSeconds waitForSeconds, Color color)
	{
		_spriteRenderer.color = color;
		yield return waitForSeconds;

		_spriteRenderer.color = _defaultColor;
	}
}