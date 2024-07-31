using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Detector<>))]
public class Attacker<T> : MonoBehaviour where T : MonoBehaviour, IAttackable, IDetectable
{
    [SerializeField, Min(1)] private float _damage = 10f;

    protected Rigidbody2D Rigidbody;

    protected float DamageZoneOffset;

    private Detector<T> _detector;

    private T _attacked;

    private bool _isNecessaryZoneToMakeDamage;

    protected virtual void Awake()
    {
        _detector = GetComponent<Detector<T>>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _detector.DetectedWithCollisionInformation += DeterminePointOfCollision;
        _detector.DetectedWitDetected += (attacked) => _attacked = attacked;
    }

    private void OnDisable()
    {
        _detector.DetectedWithCollisionInformation -= DeterminePointOfCollision;
        _detector.DetectedWitDetected -= (attacked) => _attacked = attacked;
    }

    public void Attack()
    {
        if (_isNecessaryZoneToMakeDamage)
            _attacked.TakeDamage(_damage);

        _isNecessaryZoneToMakeDamage = false;
    }

    protected virtual void DetermineCanDamageDone(Collision2D collision, Vector2 contactPoint, float middleYOfAttacked)
    {
        float contactPointY = contactPoint.y;

        if (contactPointY > middleYOfAttacked + DamageZoneOffset)
            _isNecessaryZoneToMakeDamage = true;
    }

    private void DeterminePointOfCollision(Collision2D collision)
    {
        int firstContactIndex = 0;
        Vector2 contactPoint = collision.GetContact(firstContactIndex).point;
        float middleYOfAttacked = collision.collider.bounds.center.y;

        DetermineCanDamageDone(collision, contactPoint, middleYOfAttacked);
    }
}