using UnityEngine;

public static class Mover
{
    public static void MoveHorizontally(Rigidbody2D rigidbody, float x)
    {
        Vector2 movement = new Vector2(x * Time.fixedDeltaTime, rigidbody.velocity.y);
        rigidbody.velocity = movement;
    }

    public static void MoveVertically(Rigidbody2D rigidbody, float y)
    {
        Vector2 movement = new Vector2(rigidbody.velocity.x, y * Time.fixedDeltaTime);
        rigidbody.velocity = movement;
    }
}