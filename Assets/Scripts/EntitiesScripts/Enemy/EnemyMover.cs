using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStander), typeof(Collider2D))]
public class EnemyMover : MonoBehaviour
{
	[SerializeField, Min(1)] private float _movementSpeed = 50f;
	[SerializeField, Min(1)] private float _standingForSeconds = 2f;

	private Collider2D _collider;
	private Rigidbody2D _rigidbody;
	
	private EnemyStander _stander;

	private WaitForFixedUpdate _waitForFixedUpdate;
	private WaitForSeconds _waitForSeconds;

	private int _oppositeMovement = -1;

	private bool _shouldReachRightEdge;
	private bool _shouldReachLeftEdge;
	
	private float _rightEdge;
	private float _leftEdge;

	public event Action<bool> IsStanding;
	public event Action<bool> IsWalking;
	public event Action<float> ChangedDirection;
	
	public float RightEdge => _rightEdge;
	public float LeftEdge => _leftEdge;

	private void Awake()
	{
		_stander = GetComponent<EnemyStander>();

		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();

		_waitForFixedUpdate = new WaitForFixedUpdate();
		_waitForSeconds = new WaitForSeconds(_standingForSeconds);
	}

	private void OnEnable()
	{
		_stander.IsStanding += isStanding => IsStanding?.Invoke(isStanding);

		_shouldReachRightEdge = true;
	}

	private void OnDisable()
	{
		_stander.IsStanding -= isStanding => IsStanding?.Invoke(isStanding);
	}

	public void Move(float leftmostX, float rightmostX)
	{
		if (_stander.IsOnPlatform)
		{
			if (_shouldReachRightEdge)
				StartCoroutine(MoveRight(rightmostX));

			else if (_shouldReachLeftEdge)
				StartCoroutine(MoveLeft(leftmostX));
		}
	}

	private IEnumerator MoveRight(float rightmostX)
	{
		ChangedDirection?.Invoke(_movementSpeed);
		IsWalking?.Invoke(_shouldReachRightEdge);

		PrepareInAdvanceToInspect();

		_rightEdge = Mathf.Floor(rightmostX - _collider.bounds.extents.x);

		while (transform.position.x < _rightEdge)
		{
			Mover.MoveHorizontally(_rigidbody, _movementSpeed);

			yield return _waitForFixedUpdate;
		}

		IsWalking?.Invoke(_shouldReachRightEdge);

		yield return StartCoroutine(LookAround());

		_shouldReachLeftEdge = true;
	}

	private IEnumerator MoveLeft(float leftmostX)
	{
		float x = _movementSpeed * _oppositeMovement;

		ChangedDirection?.Invoke(x);
		IsWalking?.Invoke(_shouldReachLeftEdge);

		PrepareInAdvanceToInspect();

		_leftEdge = Mathf.Ceil(leftmostX + _collider.bounds.extents.x);

		while (transform.position.x > _leftEdge)
		{
			Mover.MoveHorizontally(_rigidbody, x);

			yield return _waitForFixedUpdate;
		}

		IsWalking?.Invoke(_shouldReachLeftEdge);

		yield return StartCoroutine(LookAround());

		_shouldReachRightEdge = true;
	}

	private void PrepareInAdvanceToInspect()
	{
		_shouldReachRightEdge = false;
		_shouldReachLeftEdge = false;
	}

	private IEnumerator LookAround()
	{
		yield return _waitForSeconds;
	}
}