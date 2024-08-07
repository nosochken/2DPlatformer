using UnityEngine;

public class PhysicalPlayer : PhysicsCustomizer<Player>
{
    private const string PlayerLayer = "Player";

    private Rigidbody2D _rigidbody;

    protected override void GetComponents()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void ConfigureComponents()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        gameObject.layer = LayerMask.NameToLayer(PlayerLayer);
    }
}