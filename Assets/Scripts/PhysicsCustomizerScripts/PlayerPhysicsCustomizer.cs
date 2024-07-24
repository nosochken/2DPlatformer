using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerPhysicsCustomizer : PhysicsCustomizer<PhysicalPlayer>
{
    protected override void ConfigureComponents()
    {
        base.ConfigureComponents();

        Rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
}