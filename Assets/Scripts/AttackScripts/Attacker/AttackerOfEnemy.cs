using UnityEngine;

[RequireComponent(typeof(AttackedEnemyDetector))]
public class AttackerOfEnemy : Attacker<AttackedEnemy>
{
    [SerializeField, Min(0)] private float _damageZoneOffset = 0.5f;

    protected override void CanDamageDone(AttackedEnemy attacked, float damageZoneOffset = 0f)
    {
        base.CanDamageDone(attacked, _damageZoneOffset);
    }
}