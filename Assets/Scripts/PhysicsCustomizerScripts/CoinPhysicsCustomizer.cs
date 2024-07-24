using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class CoinPhysicsCustomizer : PhysicsCustomizer<PhysicalCoin>
{
    private const string CoinLayer = "Coin";

    protected override void ConfigureComponents()
    {
        base.ConfigureComponents();

        Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

        gameObject.layer = LayerMask.NameToLayer(CoinLayer);
    }
}