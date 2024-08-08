using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationsChanger), typeof(PhysicalPlayer), typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMover), typeof(DetectPlayer), typeof(AttackerOfEnemy))]
[RequireComponent(typeof(PlayerCoinCollector), typeof(AttackedPlayer), typeof(HealablePlayer))]
[RequireComponent(typeof(ColorChanger))]
public class Player : MonoBehaviour, ILivable
{
	private PlayerMover _mover;
	private AttackerOfEnemy _enemyAttacker;

	public event Action<bool> IsStanding;
	public event Action<float> ChangedDirection;
	public event Action<bool> IsRunning;
	public event Action<bool> IsJumping;

	private void Awake()
	{
		_mover = GetComponent<PlayerMover>();
		_enemyAttacker = GetComponent<AttackerOfEnemy>();
	}

	private void OnEnable()
	{
		_mover.IsStanding += isStanding => IsStanding.Invoke(isStanding);
		_mover.ChangedDirection += direction => ChangedDirection?.Invoke(direction);
		_mover.IsRunning += isRunning => IsRunning?.Invoke(isRunning);
		_mover.IsJumping += isJumping => IsJumping?.Invoke(isJumping);
	}

	private void FixedUpdate()
	{
		_mover.Move();
		_enemyAttacker.Attack();
	}

	private void OnDisable()
	{
		_mover.IsStanding -= isStanding => IsStanding.Invoke(isStanding);
		_mover.ChangedDirection -= direction => ChangedDirection?.Invoke(direction);
		_mover.IsRunning -= isRunning => IsRunning?.Invoke(isRunning);
		_mover.IsJumping -= isJumping => IsJumping?.Invoke(isJumping);
	}
}