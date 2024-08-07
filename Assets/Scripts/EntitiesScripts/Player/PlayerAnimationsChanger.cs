using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Player))]
public class PlayerAnimationsChanger : MonoBehaviour
{
	private Animator _animator;
	private Player _player;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_player = GetComponent<Player>();
	}

	private void OnEnable()
	{
		_player.IsStanding += ChangeStandingState;
		_player.ChangedDirection += FlipPlayer;
		_player.IsRunning += ChangeRunningState;
		_player.IsJumping += ChangeJumpingState;
	}

	private void OnDisable()
	{
		_player.IsStanding -= ChangeStandingState;
		_player.ChangedDirection -= FlipPlayer;
		_player.IsRunning -= ChangeRunningState;
		_player.IsJumping -= ChangeJumpingState;
	}

	private void ChangeStandingState(bool isStanding)
	{
		_animator.SetBool(PlayerAnimatorData.Params.IsStanding, isStanding);
	}

	private void ChangeRunningState(bool isRunning)
	{
		_animator.SetBool(PlayerAnimatorData.Params.IsRunning, isRunning);
	}

	private void ChangeJumpingState(bool isJumping)
	{
		_animator.SetBool(PlayerAnimatorData.Params.IsJumping, isJumping);
	}

	private void FlipPlayer(float direction)
	{
		_player.gameObject.TryGetComponent(out SpriteRenderer playerSpriteRenderer);

		if (playerSpriteRenderer != null)
			playerSpriteRenderer.flipX = direction < 0;
	}
}