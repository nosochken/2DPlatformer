using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class HealablePlayer : MonoBehaviour
{
    private PlayerHealth _health;
    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
    }

    public void Heal(float health)
    {
        _health.Recover(health);
    }
}