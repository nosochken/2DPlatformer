using UnityEngine;

[RequireComponent(typeof(AttackedPlayerDetector))]
public class PlayerAttacker : Attacker<AttackedPlayer>
{
	[SerializeField, Min(0)] private float _damageZoneOffset = -0.5f;
	[SerializeField, Min(1)] private float _repelForce = 4f;

	protected override void DetermineCanDamageDone(AttackedPlayer attackedPlayer, float damageZoneOffset = 0f)
	{
		base.DetermineCanDamageDone(attackedPlayer, _damageZoneOffset);

		Repel(attackedPlayer);
	}

	private void Repel(AttackedPlayer attackedPlayer)
	{
		if (attackedPlayer.gameObject.TryGetComponent(out Rigidbody2D attackedPlayerRigidbody))
		{
			Vector2 repelDirection = ((Vector2)attackedPlayer.transform.position - GetPointOfContact()).normalized;
			attackedPlayerRigidbody.velocity = Vector2.zero;
			
			attackedPlayer.BecomeImmobile();
			
			attackedPlayerRigidbody.AddForce(repelDirection * _repelForce, ForceMode2D.Impulse);
		}
	}
}