using UnityEngine;

[RequireComponent(typeof(AttackedEnemyDetector))]
public class AttackerOfEnemy : Attacker<AttackedEnemy>
{
    [SerializeField, Min(0)] private float _damageZoneOffset = 0.5f;

    protected override bool CanDamageDoneWithOffset(AttackedEnemy attacked, float damageZoneOffset = 0)
    {
        return base.CanDamageDoneWithOffset(attacked, _damageZoneOffset);

    }
}