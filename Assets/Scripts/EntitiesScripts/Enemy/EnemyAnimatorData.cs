using UnityEngine;

public static class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int IsStanding = Animator.StringToHash(nameof(IsStanding));
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
    }
}
