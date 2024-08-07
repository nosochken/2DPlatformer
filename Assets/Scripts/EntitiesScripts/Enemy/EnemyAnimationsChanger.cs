using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Enemy))]
public class EnemyAnimationsChanger : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.IsStanding += ChangeStandingState;
        _enemy.IsWalking += ChangeWalkingState;
        _enemy.ChangedDirection += FlipEnemy;
    }

    private void OnDisable()
    {
        _enemy.IsStanding -= ChangeStandingState;
        _enemy.IsWalking -= ChangeWalkingState;
        _enemy.ChangedDirection -= FlipEnemy;
    }

    private void ChangeStandingState(bool isStanding)
    {
        _animator.SetBool(EnemyAnimatorData.Params.IsStanding, isStanding);
    }

    private void ChangeWalkingState(bool isWalking)
    {
        _animator.SetBool(EnemyAnimatorData.Params.IsWalking, isWalking);
    }

    private void FlipEnemy(float direction)
    {
        _enemy.gameObject.TryGetComponent(out SpriteRenderer enemySpriteRenderer);

        if (enemySpriteRenderer != null)
            enemySpriteRenderer.flipX = direction < 0;
    }
}