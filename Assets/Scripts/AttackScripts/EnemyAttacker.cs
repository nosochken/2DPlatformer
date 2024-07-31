using UnityEngine;

[RequireComponent(typeof(AttackedEnemyDetector))]
public class EnemyAttacker : Attacker<AttackedEnemy>
{	
	protected override void Awake()
	{
		base.Awake();
		
		DamageZoneOffset = 0.5f;
	}
}
