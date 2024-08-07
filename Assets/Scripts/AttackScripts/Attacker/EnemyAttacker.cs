using UnityEngine;

[RequireComponent(typeof(AttackedEnemyDetector))]
public class EnemyAttacker : Attacker<AttackedEnemy>
{
    [SerializeField, Min(0)] private float _damageZoneOffset = 0.5f;

    protected override void DetermineCanDamageDone(AttackedEnemy attacked, float damageZoneOffset = 0f)
    {
        base.DetermineCanDamageDone(attacked, _damageZoneOffset);
    }
}