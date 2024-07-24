using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsCustomizer<T> : MonoBehaviour where T : MonoBehaviour, IPhysicable
{
    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        GetComponents();
        ConfigureComponents();
    }

    protected virtual void GetComponents()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void ConfigureComponents()
    {
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}