using UnityEngine;

public class PlayerHealth : Health
{
    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Recover(float value)
    {
        Increase(value);
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