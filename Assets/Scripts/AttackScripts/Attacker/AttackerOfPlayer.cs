using UnityEngine;

[RequireComponent(typeof(AttackedPlayerDetector))]
public class AttackerOfPlayer : Attacker<AttackedPlayer>
{
	[SerializeField, Min(0)] private float _damageZoneOffset = -0.5f;
	[SerializeField, Min(1)] private float _repelForce = 4f;

	protected override bool CanDamageDoneWithOffset(AttackedPlayer attackedPlayer, float damageZoneOffset = 0)
	{
		Repel(attackedPlayer);
		
		return base.CanDamageDoneWithOffset(attackedPlayer, _damageZoneOffset);
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