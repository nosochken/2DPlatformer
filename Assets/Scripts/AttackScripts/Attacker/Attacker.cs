using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Detector<>))]
public class Attacker<T> : MonoBehaviour where T : MonoBehaviour, IAttackable, IDetectable
{
	[SerializeField, Min(1)] private float _damage = 10f;

	private Detector<T> _detector;
	private Collision2D _collision;
	private T _attacked;

	private bool _isNecessaryZoneToMakeDamage;

	private void Awake()
	{
		_detector = GetComponent<Detector<T>>();
	}

	private void OnEnable()
	{
		_detector.DetectedWithCollisionInformation += SetCollisionInformation;
		_detector.DetectedWitDetected += (attacked) => _attacked = attacked;
	}

	private void OnDisable()
	{
		_detector.DetectedWithCollisionInformation -= SetCollisionInformation;
		_detector.DetectedWitDetected -= (attacked) => _attacked = attacked;
	}

	public void Attack()
	{
		if (_isNecessaryZoneToMakeDamage)
			_attacked.TakeDamage(_damage);
	
		_isNecessaryZoneToMakeDamage = false;
	}

	protected virtual void DetermineCanDamageDone(T attacked, float damageZoneOffset = 0f)
	{
		float contactPointY = GetPointOfContact().y;
		float middleYOfAttacked = _collision.collider.bounds.center.y;

		if (contactPointY > middleYOfAttacked + damageZoneOffset)
			_isNecessaryZoneToMakeDamage = true;
	}
	
	protected Vector2 GetPointOfContact()
	{
		int firstContactIndex = 0;
		return _collision.GetContact(firstContactIndex).point;
	}

	private void SetCollisionInformation(Collision2D collision)
	{
		_collision = collision;
		
		DetermineCanDamageDone(_attacked);
	}
}