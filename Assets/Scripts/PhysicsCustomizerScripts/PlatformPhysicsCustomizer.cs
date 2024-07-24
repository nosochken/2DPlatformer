using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D), typeof(CompositeCollider2D), typeof(PlatformEffector2D))]
public class PlatformPhysicsCustomizer : PhysicsCustomizer<PhysicalPlatform>
{
    private TilemapCollider2D _tilemapCollider;
    private CompositeCollider2D _compositeCollider;
    private PlatformEffector2D _platformEffector;

    protected override void GetComponents()
    {
        base.GetComponents();

        _tilemapCollider = GetComponent<TilemapCollider2D>();
        _compositeCollider = GetComponent<CompositeCollider2D>();

        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    protected override void ConfigureComponents()
    {
        Rigidbody.bodyType = RigidbodyType2D.Static;

        _tilemapCollider.usedByComposite = true;
        _compositeCollider.usedByEffector = true;

        _platformEffector.useOneWay = true;
        _platformEffector.useOneWayGrouping = true;
    }
}