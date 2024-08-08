using UnityEngine;

[RequireComponent(typeof(Health))]
public class Attacked<T> : Detected<T>, IAttackable where T : MonoBehaviour, ILivable
{
    private Health _health;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void TakeDamage(float damage)
    {
        _health.Decrease(damage);
    }
}