using UnityEngine;

[RequireComponent(typeof(AttackedPlayerDetector))]
public class AttackerOfPlayer : Attacker<AttackedPlayer>
{
    [SerializeField, Min(0)] private float _damageZoneOffset = -0.5f;
    [SerializeField, Min(1)] private float _repelForce = 4f;

    protected override void CanDamageDone(AttackedPlayer attackedPlayer, float damageZoneOffset = 0f)
    {
        base.CanDamageDone(attackedPlayer, _damageZoneOffset);

        Repel(attackedPlayer);
    }

    private void Repel(AttackedPlayer attackedPlayer)
    {
        if (attackedPlayer.gameObject.TryGetComponent(out Rigidbody2D attackedPlayerRigidbody))
        {
            Vector2 repelDirection = ((Vector2)attackedPlayer.transform.position - GetContactPoint()).normalized;
            attackedPlayerRigidbody.velocity = Vector2.zero;

            attackedPlayer.BecomeImmobile();

            attackedPlayerRigidbody.AddForce(repelDirection * _repelForce, ForceMode2D.Impulse);
        }
    }
}