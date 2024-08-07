using UnityEngine;

public static class PlayerAnimatorData
{
	public static class Params
	{
		public static readonly int IsStanding = Animator.StringToHash(nameof(IsStanding));
		public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
		public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
	}
}