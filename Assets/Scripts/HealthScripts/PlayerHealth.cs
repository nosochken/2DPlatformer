using UnityEngine;

public class PlayerHealth : Health
{
    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    protected override void TryDie()
    {
        base.TryDie();

        if (IsDead)
        {
            Restore();
            transform.position = _startPosition;
        }
    }
}