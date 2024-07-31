using UnityEngine;

[RequireComponent(typeof(AttackedPlayerDetector))]
public class PlayerAttacker : Attacker<AttackedPlayer>
{
    [SerializeField, Min(1)] private float _repelForce = 4f;

    protected override void Awake()
    {
        base.Awake();

        DamageZoneOffset = -0.5f;
    }

    protected override void DetermineCanDamageDone(Collision2D collision, Vector2 contactPoint, float middleYOfAttacked)
    {
        base.DetermineCanDamageDone(collision, contactPoint, middleYOfAttacked);

        Repel(collision, contactPoint);
    }

    private void Repel(Collision2D collision, Vector2 contactPoint)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rigidbodyOfAttacked))
        {
            Vector2 repelDirection = ((Vector2)collision.transform.position - contactPoint).normalized;
            rigidbodyOfAttacked.velocity = repelDirection * _repelForce;
        }
    }
}