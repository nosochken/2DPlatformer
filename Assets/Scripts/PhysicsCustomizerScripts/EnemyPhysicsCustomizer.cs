using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyPhysicsCustomizer : PhysicsCustomizer<PhysicalEnemy>
{
    private const string EnemyLayer = "Enemy";

    protected override void ConfigureComponents()
    {
        base.ConfigureComponents();

        gameObject.layer = LayerMask.NameToLayer(EnemyLayer);
    }
}