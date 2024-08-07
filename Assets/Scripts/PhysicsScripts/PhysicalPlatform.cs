using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D), typeof(CompositeCollider2D), typeof(PlatformEffector2D))]
public class PhysicalPlatform : PhysicsCustomizer<Platform>
{
    private Rigidbody2D _rigidbody;

    private TilemapCollider2D _tilemapCollider;
    private CompositeCollider2D _compositeCollider;

    private PlatformEffector2D _platformEffector;

    protected override void GetComponents()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _tilemapCollider = GetComponent<TilemapCollider2D>();
        _compositeCollider = GetComponent<CompositeCollider2D>();

        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    protected override void ConfigureComponents()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;

        _tilemapCollider.usedByComposite = true;
        _compositeCollider.usedByEffector = true;

        _platformEffector.useOneWay = true;
        _platformEffector.useOneWayGrouping = true;
    }
}