using UnityEngine;

[RequireComponent(typeof(Health<>))]
public class Attacked<T> : Detection<T>, IAttackable where T : MonoBehaviour, ILivable
{
    private Health<T> _health;

    protected virtual void Awake()
    {
        _health = GetComponent<Health<T>>();
    }

    public void TakeDamage(float damage)
    {
        _health.Decrease(damage);
    }
}