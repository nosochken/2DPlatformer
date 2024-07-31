using UnityEngine;

[RequireComponent(typeof(Health<>))]
public class Attacked<T> : Detect<T>, IAttackable where T : MonoBehaviour, ILivable
{
	private Health<T> _health;
	
	private void Awake()
	{
		_health = GetComponent<Health<T>>();
	}
	
	public void TakeDamage(float damage)
	{
		_health.Decrease(damage);
	}
}