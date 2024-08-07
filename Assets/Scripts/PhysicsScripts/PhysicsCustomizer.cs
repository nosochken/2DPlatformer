using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PhysicsCustomizer<T> : MonoBehaviour, IPhysicable where T : MonoBehaviour
{
    private void Awake()
    {
        GetComponents();
        ConfigureComponents();
    }

    protected abstract void GetComponents();

    protected abstract void ConfigureComponents();
}