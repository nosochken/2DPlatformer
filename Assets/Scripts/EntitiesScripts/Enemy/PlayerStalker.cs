using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(PlayerDetector))]
public class PlayerStalker : MonoBehaviour
{
	private const string PlayerLayer = "Player";

	[SerializeField] private float _maxPlayerDetectionDistance = 3f;
	[SerializeField] private float _stalkingSpeed = 2f;

	private EnemyMover _mover;
	private PlayerDetector _playerDetector;
	
	DetectPlayer _detectPlayer;

	private Vector2 _direction;

	private bool _isStalking;

	public bool IsStalking => _isStalking;

	private void Awake()
	{
		_mover = GetComponent<EnemyMover>();
		_playerDetector = GetComponent<PlayerDetector>();

		_direction = transform.right;
	}

	private void OnEnable()
	{
		_mover.ChangedDirection += ChangeDirectionToOpposite;
	}

	private void OnDisable()
	{
		_mover.ChangedDirection -= ChangeDirectionToOpposite;

		_isStalking = false;
	}

	public bool CanStalk()
	{
		int playerLayer = LayerMask.GetMask(PlayerLayer);
		Vector2 origin = transform.position;

		_detectPlayer = _playerDetector.DetectViaRaycast(
			playerLayer, origin, _direction, _maxPlayerDetectionDistance);

		if (_detectPlayer != null)
			return true;
		
		return false;
	}

	public void Stalk()
	{
		_isStalking = true;

		if (transform.position.x < _mover.RightEdge && transform.position.x > _mover.LeftEdge)
			transform.position = Vector2.MoveTowards(transform.position, _detectPlayer.transform.position, _stalkingSpeed * Time.fixedDeltaTime);

		_isStalking = false;
		_detectPlayer = null;
	}

	private void ChangeDirectionToOpposite(float direction)
	{
		_direction = (direction > 0) ? transform.right : -transform.right;
	}
}