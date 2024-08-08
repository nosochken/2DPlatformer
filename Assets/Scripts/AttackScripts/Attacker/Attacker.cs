using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Detector<>))]
public class Attacker<T> : MonoBehaviour where T : MonoBehaviour, IAttackable, IDetectable
{
	[SerializeField, Min(1)] private float _damage = 10f;

	private Detector<T> _detector;
	private Collision2D _collision;
	private T _attacked;

	private void Awake()
	{
		_detector = GetComponent<Detector<T>>();
	}

	private void OnEnable()
	{
		_detector.DetectedWithCollisionInformation += TryAttack;
		_detector.DetectedWitDetected += (attacked) => _attacked = attacked;
	}

	private void OnDisable()
	{
		_detector.DetectedWithCollisionInformation -= TryAttack;
		_detector.DetectedWitDetected -= (attacked) => _attacked = attacked;
	}
	
	private bool CanDamageDone()
	{
		return _attacked != null && _collision != null && CanDamageDoneWithOffset(_attacked);
	}

	private void Attack()
	{
		_attacked.TakeDamage(_damage);
	}
	
	protected virtual bool CanDamageDoneWithOffset(T attacked, float damageZoneOffset = 0f)
	{
		float contactPointY = GetContactPoint().y;
		float middleYOfAttacked = _collision.collider.bounds.center.y;

		if (contactPointY > middleYOfAttacked + damageZoneOffset)
			return true;
		
		return false;
	}

	protected Vector2 GetContactPoint()
	{
		int firstContactIndex = 0;
		return _collision.GetContact(firstContactIndex).point;
	}
	
	private void TryAttack(Collision2D collision)
	{
		_collision = collision;
		
		if (CanDamageDone())
			Attack();
	}
}