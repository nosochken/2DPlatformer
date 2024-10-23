using UnityEngine;

public class DamageButton : HealthImpactButton
{
    [SerializeField, Min(1)] private float _damage = 15f;

    protected override void Impact(Health health)
    {
        health.Decrease(_damage);
    }
}