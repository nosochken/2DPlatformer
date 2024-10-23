using UnityEngine;

public class HealButton : HealthImpactButton
{
    [SerializeField, Min(1)] private float _healing = 10f;

    protected override void Impact(Health health)
    {
        health.Increase(_healing);
    }
}